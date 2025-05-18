using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiSession
    {
        AiClient _client;
        string _modelName;

        public AiSession(AiClient client, string modelName)
        {
            _client = client;
            _modelName = modelName;
        }
    }
}
