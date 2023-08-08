using Maze.Common;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


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

    private int curLine;
    private int curCol;
    private GameObject player;

    private HubConnection _connection;


    private Guid _gameId;
    private MoveDirection direction1 = MoveDirection.Up;

    private Dictionary<Vector2Int, GameObject> _cells;

    async void Start()
    {
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

        curLine = UnityEngine.Random.Range(0, 10);
        curCol = UnityEngine.Random.Range(0, 10);

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

        _connection.On<MoveResult>("MoveResult", (name) =>
        {
            Debug.Log($"{name.Status}: ({name.Line}; {name.Column}) {name.MazeSite}");
            curLine = name.Line;
            curCol = name.Column;

            if (name.Status == MoveStatus.Success)
            {
                MovePerson(name.Line, name.Column, direction1);
            }
            else if (name.Status == MoveStatus.Winner)
            {
                int dline = 0;
                int dCol = 0;

                switch(direction1)
                {
                    case MoveDirection.Up:
                        {
                            dline -= 1;
                            break;
                        }
                    case MoveDirection.Down:
                        {
                            dline += 1;
                            break;
                        }
                    case MoveDirection.Left:
                        {
                            dCol -= 1;
                            break;
                        }
                    case MoveDirection.Right:
                        {
                            dCol += 1;
                            break;
                        }
                }

                curLine = name.Line + dline;
                curCol = name.Column + dCol;

                MovePerson(name.Line + dline, name.Column + dCol, direction1);
                Debug.Log("Winner");
            }
            else
            {
                SetWall(name.Line, name.Column, direction1);
            }
        });

        await _connection.StartAsync();
        await _connection.InvokeAsync("StartGame");
        await _connection.InvokeAsync("SetPlayer", _gameId, curLine, curCol);
    }

    public async void Update()
    {
        var direction = GetDirection();
        if (direction != MoveDirection.None)
        {
            direction1 = direction;
            await _connection.InvokeAsync("Move", _gameId, direction);
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

        return moveDirection;
    }
}
