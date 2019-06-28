using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuskBot.Handler
{
    public class ResponseAwaiter
    {
        private readonly ConcurrentDictionary<string, TaskCompletionSource<Response<string>>> _taskDictionary;
        private readonly BaseSocketClient _client;
        private readonly IConfiguration _config;
        private readonly int _timeout;
        private readonly Response<string> _timeOutResponse = new Response<string>()
        {
            Errors = new List<string>()
           {
               "The request has timed out"
           },
        };

        public ResponseAwaiter(BaseSocketClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
            _timeout = int.Parse(config["messageTimeout"]);

            _client.MessageReceived += ProcessMessage;
            _taskDictionary = new ConcurrentDictionary<string, TaskCompletionSource<Response<string>>>();
        }

        public Task<Response<string>> GetResponseFromUser(ulong userId, ulong channelId)
        {
            var key = $"{userId}{channelId}";
            var tcs = new TaskCompletionSource<Response<string>>();
            var ct = new CancellationTokenSource(_timeout);

            ct.Token.Register(() => tcs.TrySetResult(_timeOutResponse), false);

            _taskDictionary[key] = tcs;

            return tcs.Task;
        }

        private async Task ProcessMessage(SocketMessage sm)
        {
            if (sm.Content.Contains(_config["prefix"]))
            {
                //let the module command handlers handle this
                return;
            }

            var key = $"{sm.Author.Id}{sm.Channel.Id}";
            if (_taskDictionary.ContainsKey(key))
            {
                var tcs = _taskDictionary[key];
                if (tcs.TrySetResult(new Response<string>(sm.Content)))
                {
                    Console.WriteLine("tcs set successfully");
                }
                _taskDictionary.Remove(key, out tcs);
            }
        }
    }

    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
        }

        public Response()
        {
        }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Any();
    }
}
