using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IFirebaseService
    {
        Task WriteMessageAsync(string path, MessageModel message);
        Task ListenToMessagesAsync(string path, Action<MessageModel> onMessageReceived);
        Task<IEnumerable<MessageModel>> GetMessagesAsync(string path); 
    }
}