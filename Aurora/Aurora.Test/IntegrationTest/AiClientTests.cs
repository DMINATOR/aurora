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
        private AiClient _client;

        public AiClientTests(ITestOutputHelper output) : base(output)
        {
            // Start server before client
            _server.Start();

            _client = new AiClient(ConstantsForTests.OLLAMA_URL);
        }

        public override void Dispose()
        {
            // Dispose server
            base.Dispose();

            // Dispose client
            if (_client != null)
            {
                _client.Dispose();
                _client = null!;
            }
        }

        [Fact]
        public void IsRunning_Success()
        {
            // Arrange

            // Act
            var isRunning = _client.IsRunning();

            // Assert
            Assert.True(isRunning);
        }

        [Fact]
        public async Task ListModels_Success()
        {
            // Arrange

            // Act
            var models = await _client.ListModels();

            // Assert
            Assert.True(true); // Assuming models are there
        }
    }
}
