using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiClient
    {
        AiServer _server;

        public AiClient(AiServer server)
        {
            _server = server;

            // set up the client
            var uri = new Uri("http://localhost:11434");
            var ollama = new OllamaApiClient(uri);
        }

        public void ListModels()
        {

        }
    }
}
