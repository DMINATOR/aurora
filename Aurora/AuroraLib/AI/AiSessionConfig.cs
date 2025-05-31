using AuroraLib.AI.Models;

namespace AuroraLib.AI
{
    public class AiSessionConfig
    {
        public required AiModelBase Model { get; set; }

        public required string PathToServerExecutable { get; set; }

        public required string ServerEndpoint { get; set; }

        public Action<string?>? OutputSink { get; set; }

        public Action<string?>? ErrorSink { get; set; }

        public Action<string?>? TokenReceivedSink { get; set; }
    }
}
