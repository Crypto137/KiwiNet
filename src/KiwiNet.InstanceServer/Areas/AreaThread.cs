using KiwiNet.Core.Logging;
using KiwiNet.Core.System;
using System.Diagnostics;
using System.Globalization;

namespace KiwiNet.InstanceServer.Areas
{
    public enum AreaThreadState
    {
        Created,
        Starting,
        Running,
        Stopping,
        Stopped,
    }

    public class AreaThread
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly AreaManager _areaManager;

        private Thread _thread = null;

        public uint Id { get; }
        public AreaThreadState State { get; private set; } = AreaThreadState.Created;

        public AreaThread(AreaManager areaManager, uint id)
        {
            _areaManager = areaManager;
            Id = id;
        }

        public override string ToString()
        {
            return $"Id={Id}, ManagedId={_thread?.ManagedThreadId}";
        }

        /// <summary>
        /// Starts a newly created <see cref="AreaThread"/>.
        /// </summary>
        public void Start()
        {
            Debug.Assert(State == AreaThreadState.Created);
            Debug.Assert(_thread == null);

            State = AreaThreadState.Starting;

            _thread = new(Run)
            {
                Name = $"AreaThread {Id}",  // We don't have a managed id until we create the thread
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
                Priority = ThreadPriority.AboveNormal,
            };

            _thread.Start();
        }

        /// <summary>
        /// Stops an <see cref="AreaThread"/> that is currently in the <see cref="AreaThreadState.Running"/> state.
        /// </summary>
        public void Stop()
        {
            Debug.Assert(State == AreaThreadState.Running);
            Debug.Assert(_thread != null);

            State = AreaThreadState.Stopping;
        }

        /// <summary>
        /// Processes <see cref="Area"/> instances that need to be updated in a loop until this <see cref="AreaThread"/> is stopped.
        /// </summary>
        private void Run()
        {
            Debug.Assert(State == AreaThreadState.Starting);

            State = AreaThreadState.Running;

            Logger.Info($"Worker thread [{this}] started");

            while (State == AreaThreadState.Running)
                UpdateArea();

            State = AreaThreadState.Stopped;
            _thread = null;

            Logger.Info($"Worker thread [{this}] stopped");
        }

        private void UpdateArea()
        {
            Area area = _areaManager.GetAreaToUpdate();

            try
            {
                if (area != null)
                {
                    Area.Current = area;
                    area.Update();
                    Area.Current = null;
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                area.Shutdown();
                Logger.Error(e.ToString());
            }

            // Enqueue the area instance for the next update if it's still running
            if (area != null)
            {
                if (area.State == AreaState.Running || area.State == AreaState.ShuttingDown)
                    _areaManager.EnqueueAreaToUpdate(area);
                else
                    Logger.Info($"Area [{area}] is no longer running");
            }
        }
    }
}
