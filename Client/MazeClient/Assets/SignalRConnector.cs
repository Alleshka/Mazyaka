using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalRConnector
{
    public Action<Message> OnMessageReceived;
    private HubConnection _connection = null;
    public HubConnection Connection => _connection;

    public async Task InitAsync<T>(string url, string handlerMethod) where T : Message
    {
        _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        _connection.On<T>(handlerMethod, (message) =>
        {
            OnMessageReceived?.Invoke(message);
        });
        await StartConnectionAsync();
    }

    public async Task SendMessageAsync<T>(T message) where T : Message
    {
        try
        {
            await _connection.InvokeAsync("SendMessage", message);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }

    private async Task StartConnectionAsync()
    {
        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }
}

[System.Serializable]
public class Message
{
    [SerializeField]
    private string _content = "";
    public string Content { get => _content; set => _content = value; }
}
