using Aurora.Test.Constants;
using AuroraLib.AI;
using AuroraLib.AI.Models;
using Xunit.Abstractions;

namespace Aurora.Test.IntegrationTest
{
    public class AiSessionTests : IDisposable
    {
        private AiSession _session;
        private ITestOutputHelper _output;
        private ICollection<string> _tokenReceived = new List<string>();

        public AiSessionTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new AiSessionConfig
            {
                PathToServerExecutable = DirectoryPathsForTests.GetOllamaPath(),
                Model = new Phi4AiModel(),
                ServerEndpoint = ConstantsForTests.OLLAMA_URL,
                ErrorSink = (message) =>
                {
                    if (message != null)
                    {
                        _output.WriteLine($"[ERROR]: {message}");
                    }
                },
                OutputSink = (message) =>
                {
                    if (message != null)
                    {
                        _output.WriteLine($"[OUTPUT]: {message}");
                    }
                },
                TokenReceivedSink = (token) =>
                {
                    if (token != null)
                    {
                        _tokenReceived.Add(token); // append tokens
                    }
                }
            };

            _session = new AiSession(config);
        }

        [Fact]
        public void SendMessage_Success()
        {
            // Arrange
            var message = "Hello, what are the most beautiful colors?";

            // Act
            var response = _session.SendMessage(message);
            _output.WriteLine($"Response: {response}");
            _output.WriteLine($"Tokens received: {string.Join(", ", _tokenReceived)}");

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.True(_tokenReceived.Count > 0, "No tokens were received during the session.");
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
