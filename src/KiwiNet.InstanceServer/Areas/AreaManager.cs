using KiwiNet.Core.Logging;
using KiwiNet.Core.System;
using System.Globalization;

namespace KiwiNet.InstanceServer.Areas
{
    public class AreaManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<uint, Area> _areas = new();
        private readonly PriorityQueue<Area, TimeSpan> _updateQueue = new();

        private Thread _workerThread;
        private bool _isRunning;
        private uint _currentInstanceId = 0;

        public void Initialize()
        {
            _isRunning = true;

            // TODO: multiple worker threads
            _workerThread= new(UpdateAreas)
            {
                Name = "AreaThread",
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
                Priority = ThreadPriority.AboveNormal,
            };

            _workerThread.Start();
        }

        public void Shutdown()
        {
            _isRunning = false;
        }

        public Area GetOrCreateArea(ref AreaSettings settings)
        {
            Area area = null;

            foreach (Area existingArea in _areas.Values)
            {
                if (existingArea.WorldAreaId == settings.WorldAreaId && existingArea.Seed == settings.Seed)
                {
                    area = existingArea;
                    break;
                }
            }

            if (area == null)
            {
                Logger.Info($"Creating area {settings.WorldAreaId} (seed={settings.Seed})...");
                settings.InstanceId = ++_currentInstanceId;
                area = new(this);
                area.Initialize(ref settings);
                _areas.Add(area.InstanceId, area);
                EnqueueAreaToUpdate(area);
            }

            return area;
        }

        public void EnqueueAreaToUpdate(Area area)
        {
            lock (_updateQueue)
                _updateQueue.Enqueue(area, area.NextUpdateTime);
        }

        private void UpdateAreas()
        {
            while (_isRunning)
                UpdateNextArea();
        }

        private void UpdateNextArea()
        {
            Area area = GetNextArea();

            try
            {
                if (area != null)
                {
                    area.Update();
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }

        private Area GetNextArea()
        {
            TimeSpan now = Clock.UnixTime;

            lock (_updateQueue)
            {
                if (_updateQueue.TryPeek(out Area area, out TimeSpan updateTime))
                {
                    if (now >= updateTime)
                        return _updateQueue.Dequeue();
                }
            }

            return null;
        }
    }
}
