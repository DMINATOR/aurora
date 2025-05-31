using OllamaSharp;
using OllamaSharp.Models;
using System.Diagnostics;
using System.Text;
using System.IO;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="AiClient"/> class.
        /// </summary>
        /// <param name="serverEndpoint">The server endpoint URI.</param>
        /// <param name="model">The model file path.</param>
        /// <exception cref="FileNotFoundException">Thrown if the model file does not exist at the specified location.</exception>
        public AiClient(string serverEndpoint, string model)
        {
            _serverEndpoint = serverEndpoint;
            var uri = new Uri(_serverEndpoint);
            _client = new OllamaApiClient(uri);
            _client.SelectedModel = model;

            if (!File.Exists(model))
            {
                throw new FileNotFoundException($"File for model not found: '{model}'");
            }
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
