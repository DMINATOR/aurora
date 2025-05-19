using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiSession : IDisposable
    {
        private readonly string _modelName;
        private AiServer _server;
        private AiClient _client;

        public AiSession(AiSessionConfig config)
        {
            _modelName = config.ModelName;

            _server = new AiServer(config.PathToServerExecutable);

            _server.ErrorSink = config.ServerErrorSink;
            _server.OutputSink = config.ServerOutputSink;

            _server.Start();

            _client = new AiClient(config.ServerEndpoint);
        }

        public void Dispose()
        {
            if(_server != null)
            {
                _server.Stop();
                _server.Dispose();
                _server = null!;
            }
        }

        public string SendMessage(string message)
        {
            var response = _client.SendMessageAsync(message).Result;
            return response;
        }
       
    }
}
