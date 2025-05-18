using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraLib.AI
{
    public class AiServer : IDisposable
    {
        private Process? _process;

        // Public delegates for output sinks
        public Action<string?>? OutputSink { get; set; }
        public Action<string?>? ErrorSink { get; set; }

        public void Dispose()
        {
            if (_process != null)
            {
                _process.Dispose();
                _process = null;
            }
        }

        public bool IsRunning()
        {
            return _process != null && !_process.HasExited;
        }

        public void Start()
        {
            if (_process != null && !_process.HasExited)
                return;

            var startInfo = new ProcessStartInfo
            {
                FileName = Constants.DirectoryPaths.GetOllamaPath(),
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
        }

        public void Stop()
        {
            if (_process != null && !_process.HasExited)
            {
                _process.Kill();
                _process.Dispose();
                _process = null;
            }
        }
    }
}
