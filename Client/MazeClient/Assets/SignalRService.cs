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

    async void Start()
    {
        //_text.text = "Hi mark";

        _cells = new Dictionary<Vector2Int, GameObject>();
        for (int line = -1; line <= 10; line++)
        {
            for (int col = -1; col <= 10; col++)
            {
                var cell = Instantiate(Floor, new Vector3(col, -line, 1), Quaternion.identity);
                var vector = new Vector2Int(line, col);
                _cells.Add(vector, cell);
            }
        }

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
            var vector = new Vector2Int(curLine, curCol);
            var cell = _cells[vector];

            player = Instantiate(person, new Vector3(cell.transform.position.x, cell.transform.position.y, -1), Quaternion.identity);
            Debug.Log(t);
        });

        _connection.On<MoveResult>("MoveResult", (result) =>
        {
            var name = result;
            curLine = name.Point.Row;
            curCol = name.Point.Column;
            Debug.Log($"{name.Status}: ({name.Point.Row}; {name.Point.Column}) {name.MazeSite}");

            if (name.Status == MoveStatus.Success)
            {
                MovePerson(name.Point.Row, name.Point.Column, direction1);
            }
            else if (name.Status == MoveStatus.Winner)
            {
                MovePerson(name.Point.Row, name.Point.Column, direction1);
                Debug.Log("Winner");

                _isGameEnded = true;
                CreateWinnerCanvas();
            }
            else
            {
                SetWall(name.Point.Row, name.Point.Column, direction1);
            }
        });

        await _connection.StartAsync();
        await _connection.InvokeAsync("StartGame");
        await _connection.InvokeAsync("SetPlayer", _gameId, curLine, curCol);
    }

    public async void Update()
    {
        if (!_isGameEnded)
        {
            var direction = GetDirection();
            if (direction != MoveDirection.None)
            {
                direction1 = direction;
                await _connection.InvokeAsync("Move", _gameId, _gameId, direction);
            }
        }
    }

    private void SetWall(int line, int col, MoveDirection direction)
    {
        float x = 0;
        float y = 0;

        GameObject block = null;

        switch (direction)
        {
            case MoveDirection.Left:
                {
                    block = VerticalWall;
                    x = -0.5f;
                    break;
                }
            case MoveDirection.Up:
                {
                    block = HorizontalWall;
                    y = 0.5f;
                    break;
                }
            case MoveDirection.Down:
                {
                    block = HorizontalWall;
                    y = -0.5f;
                    break;
                }
            case MoveDirection.Right:
                {
                    block = VerticalWall;
                    x = 0.5f;
                    break;
                }
        }

        var vector = new Vector2Int(line, col);
        var cell = _cells[vector];

        var obj = Instantiate(block, cell.transform);
        obj.transform.localPosition = new Vector3(x, y, -2);
    }

    private void MovePerson(int line, int col, MoveDirection direction)
    {
        var vector = new Vector2Int(line, col);
        var cell = _cells[vector];
        var pos = cell.transform.position;

        player.transform.position = new Vector3(pos.x, pos.y, -1);

        curLine = line;
        curCol = col;
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
        textComponent.color = Color.white;

        // Set the Text's RectTransform properties
        textTransform.anchoredPosition = Vector2.zero;
        textTransform.sizeDelta = new Vector2(200, 50);

        // Optional: Add a ContentSizeFitter component to handle text resizing
        ContentSizeFitter sizeFitter = textGO.AddComponent<ContentSizeFitter>();
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
