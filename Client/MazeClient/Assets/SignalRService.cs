using Maze.Common;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignalRService : MonoBehaviour
{
    [SerializeField]
    public GameObject VerticalWall;

    [SerializeField]
    public GameObject HorizontalWall;

    [SerializeField]
    public GameObject Floor;

    [SerializeField]
    public GameObject person;

    [SerializeField]
    public int StartLine = 0;

    [SerializeField]
    public int StartCol = 0;

    public TextMeshPro _text;

    private bool _isGameEnded = false;

    private int curLine;
    private int curCol;
    private GameObject player;

    private HubConnection _connection;


    private Guid _gameId;
    private MoveDirection direction1 = MoveDirection.Up;

    private Dictionary<Vector2Int, GameObject> _cells;

    private IMazeRenderer _renderer;

    async void Start()
    {
        _renderer = new SimpleMazeRenderer(10, 10, 1f);
        _renderer.RenderField();

        curLine = 0; // UnityEngine.Random.Range(0, 10);
        curCol = 0; // UnityEngine.Random.Range(0, 10);

        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5035/Game")
            .Build();

        _connection.On<Guid>("StartGame", (id) =>
        {
            _gameId = id;
            Debug.Log("Game id = " + id);
        });

        _connection.On<bool>("SetPlayer", (t) =>
        {
            player = _renderer.InitPlayer(person, curLine, curCol);
        });

        _connection.On<MoveResult>("MoveResult", (result) =>
        {
            var name = result;
            Debug.Log($"{name.Status}: ({name.Point.Row}; {name.Point.Column}) {name.MazeSite}");

            if (name.Status == MoveStatus.Success)
            {
                _renderer.MovePlayer(player, curLine, curCol, name.Point.Row, name.Point.Column);
            }
            else if (name.Status == MoveStatus.Winner)
            {
                _renderer.MovePlayer(player, curLine, curCol, name.Point.Row, name.Point.Column);
                Debug.Log("Winner");

                _isGameEnded = true;
                CreateWinnerCanvas();
            }
            else
            {
                _renderer.RenderWall(name.Point.Row, name.Point.Column, direction1, "");
            }

            curLine = name.Point.Row;
            curCol = name.Point.Column;
        });

        _connection.On<bool>("DestroyWallResult", (result) =>
        {
            if (result)
            {
                _renderer.RemoveWall(curLine, curCol, direction1);
            }
        });

        await _connection.StartAsync();
        await _connection.InvokeAsync("StartGame");
        await _connection.InvokeAsync("SetPlayer", _gameId, curLine, curCol);
    }

    private bool shiftKeyPressed = false;

    public async void Update()
    {
        if (!_isGameEnded)
        {
            // Check if the left Shift key is pressed
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                shiftKeyPressed = true;
            }

            // Check if the left Shift key is released
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                shiftKeyPressed = false;
            }

            var direction = GetDirection();
            if (direction != MoveDirection.None)
            {
                direction1 = direction;
                if (!shiftKeyPressed)
                {
                    await _connection.InvokeAsync("Move", _gameId, _gameId, direction);
                }
                else
                {
                    await _connection.InvokeAsync("DestroyWall", _gameId, _gameId, direction);
                }
            }
        }
    }

    private MoveDirection GetDirection()
    {
        MoveDirection moveDirection = MoveDirection.None;

        if (!_isGameEnded)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                moveDirection = MoveDirection.Up;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                moveDirection = MoveDirection.Down;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                moveDirection = MoveDirection.Left;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                moveDirection = MoveDirection.Right;
            }

        }
        return moveDirection;
    }

    private void CreateWinnerCanvas()
    {
        // Create a new Canvas GameObject
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Add a CanvasScaler component to handle screen resolutions
        canvasGO.AddComponent<CanvasScaler>();

        // Add a GraphicRaycaster component to handle UI interactions
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create a Text GameObject
        GameObject textGO = new GameObject("WinnerText");
        RectTransform textTransform = textGO.AddComponent<RectTransform>();
        textTransform.SetParent(canvas.transform, false);

        // Add a Text component to the Text GameObject
        Text textComponent = textGO.AddComponent<Text>();
        textComponent.text = "Winner"; 

        // Set additional Text properties (font, font size, color, etc.)
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 24;
        textComponent.color = Color.red;

        // Set the Text's RectTransform properties
        textTransform.anchoredPosition = Vector2.zero;
        textTransform.sizeDelta = new Vector2(200, 50);

        // Optional: Add a ContentSizeFitter component to handle text resizing
        ContentSizeFitter sizeFitter = textGO.AddComponent<ContentSizeFitter>();
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
