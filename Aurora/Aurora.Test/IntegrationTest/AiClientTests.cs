using AuroraLib.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Aurora.Test.IntegrationTest
{
    public class AiClientTests : AiBaseClassTests
    {
        private readonly AiClient _client;

        public AiClientTests(ITestOutputHelper output) : base(output)
        {
            _client = new AiClient(_server);
        }


    }
}
