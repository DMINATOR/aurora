using AuroraLib.Constants;
using OllamaSharp;
using OllamaSharp.Models;

namespace AuroraLib.AI.Models
{
    /// <summary>
    /// Base class for defining a model for AI interactions.
    /// </summary>
    public abstract class AiModelBase
    {
        public abstract string ModelName { get; set; }

        public abstract string UserMessageStartTag { get; set; }
        public abstract string UserMessageEndTag { get; set; }

        public abstract string AssistantMessageResponseTag { get; set; }

        /// Wraps the message before being sent to the AI model.
        public abstract string WrapMessage(string message);
    }
}
