﻿using Aurora.Test.Constants;
using AuroraLib.AI;
using Xunit.Abstractions;

namespace Aurora.Test.IntegrationTest
{
    public class AiSessionTests : IDisposable
    {
        private AiSession _session;
        private ITestOutputHelper _output;

        public AiSessionTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new AiSessionConfig
            {
                PathToServerExecutable = DirectoryPathsForTests.GetOllamaPath(),
                ModelName = Constants.ConstantsForTests.OLLAMA_MODEL,
                ServerEndpoint = ConstantsForTests.OLLAMA_URL,
                ServerErrorSink = (message) =>
                {
                    if (message != null)
                    {
                        _output.WriteLine($"[ERROR]: {message}");
                    }
                },
                ServerOutputSink = (message) =>
                {
                    if (message != null)
                    {
                        _output.WriteLine($"{message}");
                    }
                }
            };

            _session = new AiSession(config);
        }

        [Fact]
        public void SendMessage_Success()
        {
            // Arrange
            var message = "<|user|>Hello, what's the most beautiful colors?<|end|><|assistant|>"; // TODO - Get these tokens from the model

            // Act
            var response = _session.SendMessage(message);
            _output.WriteLine($"Response: {response}");

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        public void Dispose()
        {
            if( _session != null )
            {
                _session.Dispose();
                _session = null!;
            }
        }
    }

}
