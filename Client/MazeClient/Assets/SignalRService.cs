using Maze.Common;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text;
using UnityEngine;


public class SignalRService : MonoBehaviour
{

    [SerializeField]
    public GameObject VerticalWall;

    [SerializeField]
    public GameObject HorizontalWall;

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

    private int _cellSize = 2;

    private Guid _gameId;
    private MoveDirection direction1 = MoveDirection.Up;

    async void Start()
    {
        curLine = StartLine;
        curCol = StartCol;

        player = Instantiate(person, new Vector3(curLine * _cellSize, curCol * _cellSize, -2), Quaternion.identity);

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

    private void SetWall (int line, int col, MoveDirection direction)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"CurLine = {line}");
        builder.AppendLine($"CurCol = {col}");

        int dLine = 0;
        int dCol = 0;

        GameObject block = null;

        switch (direction)
        {
            case MoveDirection.Left:
                {
                    block = VerticalWall;
                    dLine -= 1;
                    break;
                }
            case MoveDirection.Up:
                {
                    block = HorizontalWall;
                    dCol += 1;
                    break;
                }
            case MoveDirection.Down:
                {
                    block = HorizontalWall;
                    dCol -= 1;
                    break;
                }
            case MoveDirection.Right:
                {
                    block = VerticalWall;
                    dLine += 1;
                    break;
                }
        }

        Debug.Log(builder.ToString());
        Instantiate(block, new Vector3((curCol * _cellSize) + (dLine * _cellSize / 2), (-curLine * _cellSize) + (dCol * _cellSize / 2), -2), Quaternion.identity);
    }

    private void MovePerson(int line, int col, MoveDirection direction)
    {
        Debug.Log("Move person " + direction);

        int dLine = 0;
        int dCol = 0;

        switch (direction)
        {
            case MoveDirection.Left:
                {
                    dLine -= 1;
                    break;
                }
            case MoveDirection.Up:
                {
                    dCol += 1;
                    break;
                }
            case MoveDirection.Down:
                {
                    dCol -= 1;
                    break;
                }
            case MoveDirection.Right:
                {
                    dLine += 1;
                    break;
                }
        }

        Debug.Log(dLine + " | " + dCol);

        curLine += dLine;
        curCol += dCol;

        player.transform.Translate(dLine * _cellSize, dCol * _cellSize, 0);
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
