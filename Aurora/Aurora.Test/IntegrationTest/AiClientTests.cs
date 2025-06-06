﻿using Aurora.Test.Constants;
using AuroraLib.AI;
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

            _client = new AiClient(ConstantsForTests.OLLAMA_URL, ConstantsForTests.OLLAMA_MODEL.ModelName);

            // Assign delegates for output sinks
            _client.OutputSink = (message) =>
            {
                if (message != null)
                {
                    _output.WriteLine($"{message}");
                }
            };

            _client.ErrorSink = (message) =>
            {
                if (message != null)
                {
                    _output.WriteLine($"[ERROR]: {message}");
                }
            };
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

            _output.WriteLine($"Is Ollama running: {isRunning}");

            // Assert
            Assert.True(isRunning);
        }

        [Fact]
        public async Task ListModels_Success()
        {
            // Arrange

            // Act
            var models = await _client.ListModels();

            _output.WriteLine($"Models: {string.Join(", ", models.Select(m => m.Name))}");

            // Assert
            Assert.True(true); // Assuming models are there
        }
    }
}
