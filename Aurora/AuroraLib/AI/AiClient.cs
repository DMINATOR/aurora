using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiClient : IDisposable
    {
        private string _serverEndpoint;
        private OllamaApiClient _client;
        private Stopwatch _stopWatch = new Stopwatch();

        public Action<string?>? OutputSink { get; set; }
        public Action<string?>? ErrorSink { get; set; }
        public Action<string>? TokenReceivedSink { get; set; }

        public AiClient(string serverEndpoint, string model)
        {
            _serverEndpoint = serverEndpoint;
            var uri = new Uri(_serverEndpoint);
            _client = new OllamaApiClient(uri);
            _client.SelectedModel = model;
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null!;
            }
        }

        public bool IsRunning()
        {
            WriteOutputMessage($"Is Running");

            return _client.IsRunningAsync().Result;
        }

        public async Task<IEnumerable<Model>> ListModels()
        {
            WriteOutputMessage($"List models");

            var models = await _client.ListLocalModelsAsync();
            return models;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var chat = new Chat(_client);
            var builder = new StringBuilder();

            _stopWatch.Start();

            WriteOutputMessage(message);

            try
            {
                await foreach (var answerToken in chat.SendAsync(message))
                {
                    builder.Append(answerToken);
                    TokenReceivedSink?.Invoke(answerToken);
                }
            }
            catch (Exception ex)
            {
                ErrorSink?.Invoke(ex.Message);
                throw;
            }

            _stopWatch.Stop();
            var delta = _stopWatch.ElapsedMilliseconds;

            WriteOutputMessage($"Response time: {delta} ms");

            return builder.ToString();
        }

        private void WriteOutputMessage(string? data)
        {
            if (data != null)
            {
                OutputSink?.Invoke($"[Client] - {data}");
            }
        }
    }
}
