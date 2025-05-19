using OllamaSharp;
using OllamaSharp.Models;
using System;
using System.Collections.Generic;
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

        public AiClient(string serverEndpoint, string model)
        {
            _serverEndpoint = serverEndpoint;

            // set up the client
            var uri = new Uri(_serverEndpoint);
            _client = new OllamaApiClient(uri);

            // select a model which should be used for further operations
            _client.SelectedModel = model;
        }

        public void Dispose()
        {
            if(_client != null)
            {
                _client.Dispose();
                _client = null!;
            }
        }

        public bool IsRunning()
        {
            return _client.IsRunningAsync().Result;
        }

        public async Task<IEnumerable<Model>> ListModels()
        {
            var models = await _client.ListLocalModelsAsync();

            return models;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var chat = new Chat(_client);
            var builder = new StringBuilder();

            await foreach (var answerToken in chat.SendAsync(message))
                builder.Append(answerToken);

            return builder.ToString();
        }
    }
}
