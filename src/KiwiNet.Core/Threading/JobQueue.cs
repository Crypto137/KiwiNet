using KiwiNet.Core.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;

namespace KiwiNet.Core.Threading
{
    /// <summary>
    /// Processes <typeparamref name="TJob"/> instances on a dedicated worker thread.
    /// </summary>
    public class JobQueue<TJob> where TJob: Job
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly BlockingCollection<TJob> _jobs = new();

        private Thread _workerThread;
        private CancellationTokenSource _cts;

        private uint _currentJobId = 0;

        public bool Start()
        {
            Debug.Assert(_workerThread == null);
            Debug.Assert(_cts == null);

            _cts = new();

            _workerThread = new(() => ProcessJobs(_cts))
            {
                Name = $"JobQueue<{typeof(TJob).Name}>",
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
            };

            _workerThread.Start();

            return true;
        }

        public void Stop()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            _workerThread = null;
        }

        public uint Enqueue(TJob job)
        {
            uint jobId = ++_currentJobId;
            job.Id = jobId;
            _jobs.Add(job);
            return jobId;
        }

        private void ProcessJobs(CancellationTokenSource cts)
        {
            Logger.Info("Started processing jobs...");

            while (cts.IsCancellationRequested == false)
            {
                try
                {
                    TJob nextJob = _jobs.Take(cts.Token);
                    nextJob.Process();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }

            Logger.Info("Stopped processing jobs");
        }
    }
}
