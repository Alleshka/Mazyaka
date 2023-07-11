using Maze.Common;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;


public class SignalRService : MonoBehaviour
{
    private HubConnection _connection;

    async void Start()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5035/Game")
            .Build();

        _connection.On<string>("MoveResult", (name) =>
        {
            Debug.Log($"{name}");
        });

        await _connection.StartAsync();
    }

    public async void Update()
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
        else if (Input.GetKeyUp (KeyCode.LeftArrow))
        {
            moveDirection = MoveDirection.Left;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDirection = MoveDirection.Right;
        }

        if (moveDirection != MoveDirection.None)
        {
            await _connection.InvokeAsync("Move", moveDirection);
        }
    }
}
