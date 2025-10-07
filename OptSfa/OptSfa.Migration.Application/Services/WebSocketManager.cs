using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;

namespace OptSfa.Migration.Application.Services
{
    public class WebSocketManager : IWebSocketManager
    {
        private readonly List<WebSocket> _sockets = new();
        public void Add(WebSocket socket)
        {
            lock (_sockets)
            {
                _sockets.Add(socket);
            }
        }

        public async Task Broadcast(object message)
        {
            var json = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(json);
            var buffer = new ArraySegment<byte>(bytes);

            List<Task> tasks;
            lock (_sockets)
            {
                tasks = _sockets.Where(ws => ws.State == WebSocketState.Open)
                                .Select(ws => ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None))
                                .ToList();
                _sockets.RemoveAll(ws => ws.State != WebSocketState.Open);
            }
            await Task.WhenAll(tasks);
        }

        public void Remove(WebSocket socket)
        {
            lock (_sockets)
            {
                _sockets.Remove(socket);
            }
        }
    }
}