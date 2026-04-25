using System;
using Maze.Common;
using MazeGame.Maze;
using Maze.ClientService;
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

        private IClientService _client;
        private MazeGrid _grid;
        private CellNavigator _navigator;

        private Guid _gameId;
        private Guid _userId;
        private bool _ready;
        private bool _won;
        private bool _moving;

        private void Awake()
        {
            _client = new SignalRClientService(hubUrl);

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
                await _client.ConnectAsync();

                var gameResp = await _client.CreateGameAsync(mazeRows, mazeCols);
                _gameId = gameResp.GameId;
                _grid.Initialize(gameResp.Cols, cellSize);
                Debug.Log($"[GameManager] Created game {gameResp.GameId} ({gameResp.Rows}x{gameResp.Cols})");

                if (_gameId == null)
                {
                    Debug.LogError("[GameManager] Failed to create game");
                    return;
                }

                var joinResp = await _client.SetUserAsync(_gameId, startRow, startCol);
                _userId = joinResp.UserId;

                _grid.RevealCell(joinResp.CellId);
                _navigator.PlaceAt(joinResp.CellId);

                _ready = true;
                Debug.Log($"[GameManager] Ready. Game={_gameId} Player={_userId}");
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
                var resp = await _client.MoveAsync(_gameId, _userId, direction);

                if (resp.Success)
                {
                    if (resp.Win)
                    {
                        _won = true;
                        _grid.MarkExit(_navigator.CurrentCellId, direction);
                        Debug.Log("[GameManager] You won!");
                        return;
                    }

                    _grid.RevealCell(resp.CellId);
                    _navigator.MoveTo(resp.CellId);
                }
                else
                {
                    _grid.MarkWall(_navigator.CurrentCellId, resp.BlockedDirection.Value, resp.MoveBlocker);
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

        private async void ExectuteDestroyWall(int cellId, MoveDirection direction)
        {
            _moving = true;
            try
            {
                var resp = await _client.DestroyWallAsync(_gameId, _userId, direction);
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
            if (_client != null)
            {
                await _client.DisposeAsync();
            }
        }
    }
}
