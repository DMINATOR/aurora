using Xunit.Abstractions;


namespace Aurora.Test.IntegrationTest
{
    public class AiServerTests : AiBaseClassTests
    {
        public AiServerTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TestStartServer_Success()
        {
            // Arrange
            var before = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            // Act
            _server.Start();

            var after = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            Thread.Sleep(3000);

            // Assert
            Assert.True(_server.IsRunning());
            Assert.Equal(0, before);
            Assert.Equal(1, after);
        }

        [Fact]
        public void TestStartServer_ReuseServer_Success()
        {
            // Arrange
            var before = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            // Act
            _server.Start();
            Thread.Sleep(3000);

            // Act again to reuse the existing process
            _server.Start();

            var after = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            // Assert
            Assert.True(_server.IsRunning());
            Assert.Equal(0, before);
            Assert.Equal(1, after);
        }

        [Fact]
        public void TestStopServer_Success()
        {
            // Arrange
            _server.Start();
            var before = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            Thread.Sleep(3000);

            // Act
            _server.Stop();
            var after = AuroraLib.AI.AiServer.GetOllamaProcesses().Count;

            // Assert
            Assert.False(_server.IsRunning());

            Assert.Equal(1, before);
            Assert.Equal(0, after);
        }

        [Fact]
        public void GetOllamaProcesses_ReturnsProcessesWithCorrectName()
        {
            // Act
            var processes = AuroraLib.AI.AiServer.GetOllamaProcesses();

            // Assert
            Assert.All(processes, p => Assert.Equal("ollama", p.ProcessName, ignoreCase: true));
        }
    }
}
