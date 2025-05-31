using AuroraLib.AI.Models;

namespace AuroraLib.AI
{
    public class AiSession : IDisposable
    {
        private AiModelBase _model;
        private AiServer _server;
        private AiClient _client;

        public AiSession(AiSessionConfig config)
        {
            _model = config.Model;

            _server = new AiServer(config.PathToServerExecutable);

            // Start server
            _server.ErrorSink = config.ErrorSink;
            _server.OutputSink = config.OutputSink;
            _server.Start();

            // Connect via Client
            _client = new AiClient(config.ServerEndpoint, _model.ModelName); // TODO verify that model exists
            _client.OutputSink = config.OutputSink;
            _client.ErrorSink = config.ErrorSink;
            _client.TokenReceivedSink = config.TokenReceivedSink;

            _client.IsRunning();
        }

        public void Dispose()
        {
            if(_server != null)
            {
                _server.Stop();
                _server.Dispose();
                _server = null!;
            }

            if (_client != null)
            {
                _client.Dispose();
                _client = null!;
            }
        }

        public string SendMessage(string message)
        {
            var wrappedMessage = _model.WrapMessage(message); // Apply model-specific wrapping to the message

            var response = _client.SendMessageAsync(wrappedMessage).Result;
            return response;
        }
       
    }
}
