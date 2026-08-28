using KiwiNet.Core.Logging;
using KiwiNet.Core.System;

namespace KiwiNet.InstanceServer.Areas
{
    public class AreaManager
    {
        private const int NumWorkerThreads = 1;

        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<uint, Area> _areas = new();

        private readonly Dictionary<uint, AreaThread> _areaThreads = new();
        private readonly PriorityQueue<Area, TimeSpan> _updateQueue = new();

        private bool _isRunning;
        private uint _currentInstanceId = 0;
        private uint _currentThreadId = 0;

        public void Initialize()
        {
            if (_isRunning)
                throw new InvalidOperationException();

            // TODO: move thread number to config
            int numWorkerThreads = Math.Max(NumWorkerThreads, 1);
            for (int i = 0; i < numWorkerThreads; i++)
            {
                AreaThread thread = new(this, ++_currentThreadId);
                _areaThreads.Add(thread.Id, thread);
                thread.Start();
            }

            _isRunning = true;
        }

        public void Shutdown()
        {
            if (_isRunning == false)
                throw new InvalidOperationException();

            // There should be no running areas by the time this gets shut down
            lock (_updateQueue)
            {
                int gameCount = _updateQueue.Count;
                if (gameCount != 0)
                    Logger.Warn($"{gameCount} areas still need updating");
            }

            foreach (var kvp in _areaThreads)
            {
                kvp.Value.Stop();
                _areaThreads.Remove(kvp.Key);
            }

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

        public void OnAreaShutdown(Area area)
        {
            _areas.Remove(area.InstanceId);
        }

        public void EnqueueAreaToUpdate(Area area)
        {
            lock (_updateQueue)
                _updateQueue.Enqueue(area, area.NextUpdateTime);
        }

        public Area GetAreaToUpdate()
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
