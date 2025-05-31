namespace AuroraLib.AI.Models
{
    /// <summary>
    /// Model that implements Phi4 ruleset, see - https://github.com/microsoft/PhiCookBook
    /// </summary>
    public class Phi4AiModel : AiModelBase
    {
        public override string ModelName { get; set; } = "phi4-mini:latest";

        // Typical message for phi4 model would look like this:
        // "<|user|>Hello, what are the most beautiful colors?<|end|><|assistant|>";

        public override string UserMessageStartTag { get; set; } = "<|user|>";
        public override string UserMessageEndTag { get; set; } = "<|end|>";
        public override string AssistantMessageResponseTag { get; set; } = "<|assistant|>";

        public override string WrapMessage(string message)
        {
            return $"{UserMessageStartTag}{message}{UserMessageEndTag}{AssistantMessageResponseTag}";
        }
    }
}
