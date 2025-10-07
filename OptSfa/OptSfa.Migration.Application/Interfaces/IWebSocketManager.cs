using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IWebSocketManager
    {
        void Add(WebSocket socket);
        void Remove(WebSocket socket);
        Task Broadcast(object message);
    }
}