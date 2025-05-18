using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    /// <summary>
    /// I am assuming there should only be one instance running at all times. Might as well make it Singleton later?
    /// </summary>
    public class AiServer : IDisposable
    {
        private Process? _process;

        private string _pathToOllama;

        // Public delegates for output sinks
        public Action<string?>? OutputSink { get; set; }
        public Action<string?>? ErrorSink { get; set; }

        public AiServer(string pathToOllama)
        {
            _pathToOllama = pathToOllama;
            _process = null;
        }

        public void Dispose()
        {
            Stop();
        }

        public bool IsRunning()
        {
            return _process != null && !_process.HasExited;
        }

        public void Start()
        {
            if (_process != null && !_process.HasExited)
            {
                WriteOutputMessage($"[Ollama] - Cannot start - Already running");
                return;
            }

            WriteOutputMessage($"[Ollama] - Starting {_pathToOllama}");

            var startInfo = new ProcessStartInfo
            {
                FileName = _pathToOllama,
                Arguments = "serve",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _process = new Process { StartInfo = startInfo };
            _process.OutputDataReceived += (s, e) => OutputSink?.Invoke(e.Data);
            _process.ErrorDataReceived += (s, e) => ErrorSink?.Invoke(e.Data);
            _process.Start();
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();

            WriteOutputMessage($"[Ollama] - Started");
        }

        public void Stop()
        {
            if (_process != null && !_process.HasExited)
            {
                WriteOutputMessage($"[Ollama] - Stopping");
                _process.Kill();
                _process.Dispose();
                _process = null;
                WriteOutputMessage($"[Ollama] - Stopped");
            }
            else
            {
                WriteOutputMessage($"[Ollama] - Cannot stop - Already stopped");
            }
        }

        private void WriteOutputMessage(string? data)
        {
            if (data != null)
            {
                OutputSink?.Invoke(data);
            }
        }
    }
}
