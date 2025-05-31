using Aurora.Test.Constants;
using AuroraLib.AI;
using Xunit.Abstractions;


// Disable parallel test execution for this class
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Aurora.Test.IntegrationTest
{
    /// <summary>
    /// Base class for the tests
    /// </summary>
    public class AiBaseClassTests : IDisposable
    {
        protected AiServer _server;
        protected readonly ITestOutputHelper _output;

        public AiBaseClassTests(ITestOutputHelper output)
        {
            _output = output;
            _server = new AuroraLib.AI.AiServer(DirectoryPathsForTests.GetOllamaPath());

            // Assign delegates for output sinks
            _server.OutputSink = (message) =>
            {
                if (message != null)
                {
                    _output.WriteLine($"{message}");
                }
            };

            _server.ErrorSink = (message) =>
            {
                if (message != null)
                {
                    _output.WriteLine($"[ERROR]: {message}");
                }
            };

            _output.WriteLine("[TEST] - AiServer instance created.");
        }

        public virtual void Dispose()
        {
            // Attempt to close and dispose the server
            if (_server != null)
            {
                _output.WriteLine("[TEST] - Stopping and disposing AiServer...");
                _server.Stop();
                _server.Dispose();
                _server = null!;
                _output.WriteLine("[TEST] - AiServer stopped and disposed.");
            }
        }
    }
}
