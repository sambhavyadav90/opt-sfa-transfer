using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.EventStreaming;
using Newtonsoft.Json;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IFirebaseClient _client;
        private EventStreamResponse? _listenerStream;

        public FirebaseService()
        {
            var firebaseConfig = new FirebaseConfig
            {
                AuthSecret = "wsSu7kBvmbZskak0u46GacVqWBJ0Vf3i5FWUV6Lz",
                BasePath = "https://testing-16b2d-default-rtdb.firebaseio.com/"
            };
            _client = new FirebaseClient(firebaseConfig);
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesAsync(string path)
        {
            var response = await _client.GetAsync(path);
            if (response.Body == null) return Enumerable.Empty<MessageModel>();

            var data = JsonConvert.DeserializeObject<Dictionary<string, MessageModel>>(response.Body);
            return data?.Values ?? Enumerable.Empty<MessageModel>();
        }

        public async Task ListenToMessagesAsync(string path, Action<MessageModel> onMessageReceived)
        {
            // Dispose previous stream if exists
            _listenerStream?.Dispose();

            // Real-time listener using FireSharp's OnAsync with separate handlers (streams changes via Server-Sent Events)
            _listenerStream = await _client.OnAsync(path,
                added: (sender, args, context) =>
                {
                    var msg = JsonConvert.DeserializeObject<MessageModel>(args.Data ?? "{}");
                    if (msg != null)
                    {
                        onMessageReceived(msg);
                    }
                },
                changed: (sender, args, context) =>
                {
                    var msg = JsonConvert.DeserializeObject<MessageModel>(args.Data ?? "{}");
                    if (msg != null)
                    {
                        onMessageReceived(msg);
                    }
                },
                removed: (sender, args, context) =>
                {
                    // Ignore removed events or handle as needed
                });
        }

        public async Task WriteMessageAsync(string path, MessageModel message)
        {
            await _client.SetAsync(path + "/" + message.Id, message);
        }
    }
}