using Maze.Client.Abstractions;
using Maze.ClientService;
using Maze.Common;
using Maze.Common.Types;
using MazeGame.Maze;
using System;
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

        private void Awake()
        {
            var client = new SignalRClientService(hubUrl);
            _gameClient = client;
            _connectable = client;

            // MazeGrid
            var gridGO = new GameObject("MazeGridRoot");
            _grid = gridGO.AddComponent<MazeGrid>();

            // Player — yellow square, renders above cells
            var playerGO = new GameObject("Player");
            var sr = playerGO.AddComponent<SpriteRenderer>();
            sr.sprite = Sprites.Square;
            sr.color = Color.yellow;
            sr.sortingOrder = 10;
            playerGO.transform.localScale = new Vector3(cellSize * 0.5f, cellSize * 0.5f, 1f);

            _navigator = playerGO.AddComponent<CellNavigator>();
            _navigator.SetMoveSpeed(moveSpeed);
            _navigator.Initialize(_grid);

            // Camera
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

                var joinResp = await _gameClient.SetUserAsync(_gameId, startRow, startCol);
                // _userId = joinResp.UserId;

                var cell = _grid.RevealCell();
                _navigator.PlaceAt(cell.CellId);

                _ready = true;
                // Debug.Log($"[GameManager] Ready. Game={_gameId} Player={_userId}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GameManager] Startup failed: {ex.Message}");
            }
        }

        private void Update()
        {
            if (!_ready || _won || _moving) return;

            MoveDirection dir = MoveDirection.None;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) dir = MoveDirection.Up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) dir = MoveDirection.Down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) dir = MoveDirection.Left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) dir = MoveDirection.Right;

            bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (dir != MoveDirection.None)
            {
                if (shift) ExectuteDestroyWall(_navigator.CurrentCellId, dir);
                else ExecuteMove(dir);
            }
        }

        private async void ExecuteMove(MoveDirection direction)
        {
            _moving = true;
            try
            {
                var resp = await _gameClient.MoveAsync(_gameId, direction);

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
                    Debug.Log("[Need a key");
                    Debug.Log(resp.AvailableKeys.Count > 0 ? string.Join("; ", resp.AvailableKeys) : "No keys available");
                    return;
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

        private async void ExectuteDestroyWall(EntityId cellId, MoveDirection direction)
        {
            _moving = true;
            try
            {
                var resp = await _gameClient.DestroyWallAsync(_gameId, direction);
                Debug.Log($"[GameManager] Destroy wall response: success={resp.IsSuccess} connectionId={resp.ConnectionId} message='{resp.Message}' grenades={resp.Grenades}");
                if (resp.IsSuccess)
                {
                    _grid.HideWall(resp.ConnectionId);
                }
                else
                {
                    Debug.Log("[GameManager] Failed to destroy wall: " + resp.Message);
                }
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

        private async void OnDestroy()
        {
            if (_gameClient != null)
            {
                await _gameClient.DisposeAsync();
            }
        }
    }
}
