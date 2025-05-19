using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiSessionConfig
    {
        public required string ModelName { get; set; }

        public required string PathToServerExecutable { get; set; }

        public required string ServerEndpoint { get; set; }

        public Action<string?>? ServerOutputSink { get; set; }

        public Action<string?>? ServerErrorSink { get; set; }
    }
}
