using Aurora.Test.Constants;
using AuroraLib.AI;
using Xunit;
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

            // Act
            _server.Start();

            Thread.Sleep(3000);

            // Assert
            Assert.True(_server.IsRunning());
        }


        [Fact]
        public void TestStopServer_Success()
        {
            // Arrange
            _server.Start();

            Thread.Sleep(3000);

            // Act
            _server.Stop();

            // Assert
            Assert.False(_server.IsRunning());
        }
    }
}
