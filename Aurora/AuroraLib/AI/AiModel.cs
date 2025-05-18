using AuroraLib.Constants;
using OllamaSharp;
using OllamaSharp.Models;

namespace AuroraLib.AI
{
    public class AiModel
    {
        private OllamaApiClient _client;
      
        public AiModel(string path)
        {
            // set up the client
            var uri = new Uri(Constants.Constants.OLLAMA_URL);
            _client = new OllamaApiClient(uri);

            if( !_client.IsRunningAsync().Result)
            {
                throw new Exception($"Ollama is not running under '{Constants.Constants.OLLAMA_URL}'. Please start it first.");
            }
        }

        public IEnumerable<Model> ListModels()
        {
            return _client.ListLocalModelsAsync().Result;
        }
    }
}
