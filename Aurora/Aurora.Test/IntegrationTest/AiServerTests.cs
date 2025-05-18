using AuroraLib.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Aurora.Test.IntegrationTest
{
    public class AiServerTests : IDisposable
    {
        private AiServer _server;
        private readonly ITestOutputHelper _output;

        public AiServerTests(ITestOutputHelper output)
        {
            _output = output;
            _server = new AuroraLib.AI.AiServer();
            _output.WriteLine("[TEST] - AiServer instance created.");
        }

        public void Dispose()
        {
            // Attempt to close and dispose the server
            if( _server != null)
            {
                _output.WriteLine("[TEST] - Stopping and disposing AiServer...");
                _server.Stop();
                _server.Dispose();
                _server = null!;
                _output.WriteLine("[TEST] - AiServer stopped and disposed.");
            }
        }

        [Fact]
        public void TestStartServer()
        {
            // Arrange
            var server = new AuroraLib.AI.AiServer();

            // Act
            server.Start();

            // Assert
            Assert.NotNull(server);
        }


        [Fact]
        public void TestStopServer()
        {
            // Arrange
            var server = new AuroraLib.AI.AiServer();
            server.Start();

            // Act
            server.Stop();

            // Assert
            Assert.Null(server);
        }
    }
}
