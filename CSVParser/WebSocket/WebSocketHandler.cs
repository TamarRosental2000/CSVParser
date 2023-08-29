using CSVParser.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

class WebSocketHandler
{
    private readonly List<WebSocket> _sockets = new List<WebSocket>();

    public async Task AddSocket(WebSocket socket)
    {
        _sockets.Add(socket);
        await HandleWebSocket(socket);
    }

    private async Task HandleWebSocket(WebSocket socket)
    {
        byte[] buffer = new byte[1024];
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {receivedMessage}");

                // Handle the received WebSocket message here
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                _sockets.Remove(socket);
                break;
            }
        }
    }

    public async Task SendToAll(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        foreach (var socket in _sockets)
        {
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        }
    }

    public async Task RemoveSocket(WebSocket socket)
    {
        _sockets.Remove(socket);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
    }
}