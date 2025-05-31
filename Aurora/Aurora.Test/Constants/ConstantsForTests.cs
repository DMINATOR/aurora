using AuroraLib.AI.Models;

namespace Aurora.Test.Constants
{
    public static class ConstantsForTests
    {
        public const string OLLAMA_URL = "http://localhost:11434";

        public static AiModelBase OLLAMA_MODEL = new Phi4AiModel();
    }
}
