using Maze.Client.Abstractions;
using Maze.ClientService;
using Maze.Common;
using Maze.Common.Types;
using MazeGame.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeGame.Game
{
    /// <summary>
    /// Place this script on a single empty GameObject in the scene.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Network")]
        [SerializeField] private string hubUrl = "http://localhost:50774/mazehub";

        [Header("Maze")]
        [SerializeField] private int mazeRows = 10;
        [SerializeField] private int mazeCols = 10;
        [SerializeField] private int startRow = 0;
        [SerializeField] private int startCol = 0;

        [Header("Visuals")]
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private float moveSpeed = 8f;

        private MazeGrid _grid;
        private CellNavigator _navigator;

        private EntityId _gameId = EntityId.Empty;
        private bool _ready;
        private bool _won;
        private bool _moving;

        private IGameClient _gameClient;
        private IConnectable _connectable;

        // Key inventory
        private readonly List<EntityId> _inventoryKeys = new List<EntityId>();
        private EntityId? _activeKey;

        // Key selection state (shown when server returns RequiresKeySelection)
        private bool _selectingKey;
        private MoveDirection _pendingDirection;

        private void Awake()
        {
            var client = new SignalRClientService(hubUrl);
            _gameClient = client;
            _connectable = client;

            var gridGO = new GameObject("MazeGridRoot");
            _grid = gridGO.AddComponent<MazeGrid>();

            var playerGO = new GameObject("Player");
            var sr = playerGO.AddComponent<SpriteRenderer>();
            sr.sprite = Sprites.Square;
            sr.color = Color.yellow;
            sr.sortingOrder = 10;
            playerGO.transform.localScale = new Vector3(cellSize * 0.5f, cellSize * 0.5f, 1f);

            _navigator = playerGO.AddComponent<CellNavigator>();
            _navigator.SetMoveSpeed(moveSpeed);
            _navigator.Initialize(_grid);

            if (Camera.main != null)
            {
                Camera.main.orthographic = true;
                Camera.main.orthographicSize = cellSize * 5.5f;
                Camera.main.transform.position = new Vector3(0f, 0f, -10f);
                Camera.main.backgroundColor = new Color(0.08f, 0.08f, 0.08f);
            }
        }

        private async void Start()
        {
            try
            {
                await _connectable.ConnectAsync();

                var gameResp = await _gameClient.CreateGameAsync(mazeRows, mazeCols);
                _gameId = gameResp.GameId;
                _grid.Initialize(cellSize);
                Debug.Log($"[GameManager] Created game {gameResp.GameId} ({gameResp.Rows}x{gameResp.Cols})");

                if (_gameId == EntityId.Empty)
                {
                    Debug.LogError("[GameManager] Failed to create game");
                    return;
                }

                await _gameClient.SetUserAsync(_gameId, startRow, startCol);

                var cell = _grid.RevealCell();
                _navigator.PlaceAt(cell.CellId);

                _ready = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GameManager] Startup failed: {ex.Message}");
            }
        }

        private void Update()
        {
            if (!_ready || _won || _moving) return;

            // Key-selection dialog: number keys pick from available list, Escape cancels
            if (_selectingKey)
            {
                for (int i = 0; i < _inventoryKeys.Count && i < 9; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        _activeKey = _inventoryKeys[i];
                        _selectingKey = false;
                        ExecuteMove(_pendingDirection);
                        return;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _selectingKey = false;
                    _pendingDirection = MoveDirection.None;
                }
                return;
            }

            // Tab: cycle active key (none → key 1 → key 2 → … → none)
            if (Input.GetKeyDown(KeyCode.Tab) && _inventoryKeys.Count > 0)
            {
                if (!_activeKey.HasValue)
                {
                    _activeKey = _inventoryKeys[0];
                }
                else
                {
                    int idx = _inventoryKeys.IndexOf(_activeKey.Value);
                    _activeKey = (idx < 0 || idx >= _inventoryKeys.Count - 1)
                        ? (EntityId?)null
                        : _inventoryKeys[idx + 1];
                }
                return;
            }

            // Number keys 1–9: pre-select key by index
            for (int i = 0; i < _inventoryKeys.Count && i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    _activeKey = _inventoryKeys[i];
                    return;
                }
            }

            // Movement
            MoveDirection dir = MoveDirection.None;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) dir = MoveDirection.Up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) dir = MoveDirection.Down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) dir = MoveDirection.Left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) dir = MoveDirection.Right;

            bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (dir != MoveDirection.None)
            {
                if (shift) ExecuteDestroyWall(_navigator.CurrentCellId, dir);
                else ExecuteMove(dir);
            }
        }

        private async void ExecuteMove(MoveDirection direction)
        {
            _moving = true;
            var sentKey = _activeKey;
            try
            {
                var resp = await _gameClient.MoveAsync(_gameId, direction, sentKey);

                // Pickups — add keys to local inventory
                if (resp.PickedUpItems != null)
                {
                    foreach (var item in resp.PickedUpItems)
                    {
                        if (item.ItemType == "KeyRoomItem" && !_inventoryKeys.Contains(item.ItemId))
                        {
                            _inventoryKeys.Add(item.ItemId);
                            Debug.Log($"[GameManager] Picked up key {item.ItemId.Value}");
                        }
                    }
                }

                bool keyConsumed = sentKey.HasValue &&
                    (resp.Win || resp.Blocker?.BlockedConnectionType == "Sealed");
                if (keyConsumed)
                {
                    _inventoryKeys.Remove(sentKey.Value);
                    if (_activeKey.HasValue && _activeKey.Value == sentKey.Value)
                        _activeKey = null;
                }

                if (resp.IsSuccess)
                {
                    if (resp.Win)
                    {
                        _won = true;
                        _grid.MarkExit(_navigator.CurrentCellId, direction);
                        Debug.Log("[GameManager] You won!");
                        return;
                    }

                    var cell = _grid.RevealCell(direction);
                    _navigator.MoveTo(cell.CellId);
                }
                else if (resp.Blocker != null)
                {
                    _grid.MarkWall(_navigator.CurrentCellId, direction, resp.Blocker);
                }
                else if (resp.RequiresKeySelection)
                {
                    _grid.MarkExit(_navigator.CurrentCellId, direction);

                    if (resp.AvailableKeys != null && resp.AvailableKeys.Count > 0)
                    {
                        // Sync local inventory with server's authoritative list
                        _inventoryKeys.Clear();
                        _inventoryKeys.AddRange(resp.AvailableKeys);
                        _pendingDirection = direction;
                        _selectingKey = true;
                    }
                    else
                    {
                        Debug.Log("[GameManager] Exit requires a key — none collected yet");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GameManager] Move error: {ex.Message}");
            }
            finally
            {
                _moving = false;
            }
        }

        private async void ExecuteDestroyWall(EntityId cellId, MoveDirection direction)
        {
            _moving = true;
            try
            {
                var resp = await _gameClient.DestroyWallAsync(_gameId, direction);
                Debug.Log($"[GameManager] Destroy wall: success={resp.IsSuccess} grenades={resp.Grenades}");
                if (resp.IsSuccess)
                    _grid.HideWall(resp.ConnectionId);
                else
                    Debug.Log("[GameManager] Failed to destroy wall: " + resp.Message);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GameManager] Destroy wall error: {ex.Message}");
            }
            finally
            {
                _moving = false;
            }
        }

        private void OnGUI()
        {
            var style = new GUIStyle(GUI.skin.label) { fontSize = 16 };

            // Inventory panel (top-left)
            int y = 10;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(10, y, 300, 24), _inventoryKeys.Count == 0 ? "Keys: none" : $"Keys ({_inventoryKeys.Count}):", style);
            y += 24;

            for (int i = 0; i < _inventoryKeys.Count; i++)
            {
                bool isActive = _activeKey.HasValue && _activeKey.Value == _inventoryKeys[i];
                style.normal.textColor = isActive ? Color.cyan : Color.white;
                string prefix = isActive ? "► " : $"{i + 1}. ";
                GUI.Label(new Rect(10, y, 300, 22), $"{prefix}Key #{_inventoryKeys[i].Value}", style);
                y += 22;
            }

            if (_activeKey.HasValue)
            {
                style.normal.textColor = Color.cyan;
                GUI.Label(new Rect(10, y + 4, 300, 22), $"Active: Key #{_activeKey.Value.Value}", style);
            }

            // Key-selection dialog (centred)
            if (_selectingKey)
            {
                float cx = Screen.width * 0.5f;
                float cy = Screen.height * 0.5f;

                style.fontSize = 20;
                style.normal.textColor = Color.yellow;
                GUI.Label(new Rect(cx - 200, cy - 60, 400, 30), "Exit is locked — choose a key:", style);

                style.fontSize = 16;
                style.normal.textColor = Color.white;
                for (int i = 0; i < _inventoryKeys.Count && i < 9; i++)
                    GUI.Label(new Rect(cx - 200, cy - 20 + i * 24, 400, 24), $"[{i + 1}]  Key #{_inventoryKeys[i].Value}", style);

                style.normal.textColor = new Color(0.6f, 0.6f, 0.6f);
                GUI.Label(new Rect(cx - 200, cy - 20 + _inventoryKeys.Count * 24 + 8, 400, 22), "Escape — cancel", style);
            }

            // Win banner
            if (_won)
            {
                style.fontSize = 48;
                style.normal.textColor = Color.green;
                float tw = Screen.width * 0.5f;
                GUI.Label(new Rect(Screen.width * 0.5f - tw * 0.5f, Screen.height * 0.5f - 40, tw, 80), "YOU WIN!", style);
            }
        }

        private async void OnDestroy()
        {
            if (_gameClient != null)
                await _gameClient.DisposeAsync();
        }
    }
}
