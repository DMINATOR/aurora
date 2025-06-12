using System.Diagnostics;
using System.Collections.Generic;

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
                WriteOutputMessage($"Cannot start - Already running");
                return;
            }

            WriteOutputMessage($"Starting {_pathToOllama}");

            var existingProcesses = GetOllamaProcesses();

            if (existingProcesses.Count > 0)
            {
                _process = existingProcesses[0]; // Pick first from the list
                WriteOutputMessage($"Found existing process with PID: {_process.Id}. Reusing it.");
            }
            else
            {
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
            }

            _process.OutputDataReceived += (s, e) => OutputSink?.Invoke(e.Data);
            _process.ErrorDataReceived += (s, e) => ErrorSink?.Invoke(e.Data);
            _process.Start();
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();

            // Set affinity to use only CPU 0 and 1 (binary 11 = decimal 3)
#pragma warning disable CA1416 // Validate platform compatibility
            _process.ProcessorAffinity = (IntPtr)0x3;
#pragma warning restore CA1416 // Validate platform compatibility

            WriteOutputMessage($"Started process with PID: {_process.Id}");
        }

        public void Stop()
        {
            if (_process != null && !_process.HasExited)
            {
                WriteOutputMessage($"Stopping");

                _process.Kill(true);

                while(!_process.HasExited)
                {
                    WriteOutputMessage($"Waiting.");
                    System.Threading.Thread.Sleep(1000);
                }

                _process.Dispose();
                _process = null;
                WriteOutputMessage($"Stopped");
            }
            else
            {
                WriteOutputMessage($"Cannot stop - Already stopped");
            }
        }

        private void WriteOutputMessage(string? data)
        {
            if (data != null)
            {
                OutputSink?.Invoke($"[Ollama] - {data}");
            }
        }

        public static List<Process> GetOllamaProcesses()
        {
            var processes = new List<Process>();
            foreach (var process in Process.GetProcessesByName("ollama"))
            {
                processes.Add(process);
            }
            return processes;
        }
    }
}
