using KiwiNet.Core.Logging;
using KiwiNet.Core.System;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Network;

namespace KiwiNet.InstanceServer.Areas
{
    public enum AreaState
    {
        Created,
        Running,
        ShuttingDown,
        Shutdown,
    }

    public class Area
    {
        public const int TargetFrameTimeMS = 33;

        private static readonly Logger Logger = LogManager.CreateLogger();
        private static readonly TimeSpan TargetFrameTime = TimeSpan.FromMilliseconds(TargetFrameTimeMS);

        [ThreadStatic]
        internal static Area Current;

        public AreaManager AreaManager { get; }
        public GameObjectManager GameObjectManager { get; }
        public RemotePlayerManager RemotePlayerManager { get; }

        public uint InstanceId { get; private set; }
        public string WorldAreaId { get; private set; }
        public string League { get; private set; }
        public uint Seed { get; private set; }

        public AreaState State { get; private set; } = AreaState.Created;

        public TimeSpan NextUpdateTime { get; private set; } = Clock.UnixTime;
        public TimeSpan LastProcessingTime { get; private set; } = TimeSpan.Zero;
        public TimeSpan LastFrameTime { get; private set; } = TimeSpan.FromMilliseconds(TargetFrameTimeMS);
        public TimeSpan LastUpdateEndTime { get; private set; } = Clock.UnixTime;

        public Area(AreaManager areaManager)
        {
            AreaManager = areaManager;
            GameObjectManager = new(this);
            RemotePlayerManager = new(this);
        }

        public void Initialize(ref AreaSettings settings)
        {
            InstanceId = settings.InstanceId;
            WorldAreaId = settings.WorldAreaId;
            League = settings.League;
            Seed = settings.Seed;

            State = AreaState.Running;
        }

        public void Shutdown()
        {
            State = AreaState.Shutdown;
        }

        public void Update()
        {
            TimeSpan startTime = Clock.UnixTime;

            DoUpdate();

            TimeSpan endTime = Clock.UnixTime;

            if ((endTime - startTime) > TargetFrameTime)
                NextUpdateTime = endTime + TargetFrameTime;
            else
                NextUpdateTime = startTime + TargetFrameTime;

            LastProcessingTime = endTime - startTime;
            LastFrameTime = endTime - LastUpdateEndTime;
            LastUpdateEndTime = endTime;
        }

        private void DoUpdate()
        {
            RemotePlayerManager.Update();
        }
    }
}
