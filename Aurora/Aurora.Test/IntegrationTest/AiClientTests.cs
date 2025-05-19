using Aurora.Test.Constants;
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
            // Start server before client
            _server.Start();

            _client = new AiClient(ConstantsForTests.OLLAMA_URL);
        }

        [Fact]
        public async Task ListModels_Success()
        {
            // Arrange

            // Act
            var models = await _client.ListModels();

            // Assert
            Assert.Equal(2, models.Count());
        }
    }
}
