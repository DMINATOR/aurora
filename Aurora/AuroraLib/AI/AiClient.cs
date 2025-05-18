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
        }
    }
}
