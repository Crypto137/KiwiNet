using KiwiNet.Core.System;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Network;

namespace KiwiNet.InstanceServer.Areas
{
    public class Area
    {
        public const int TargetFrameTimeMS = 33;

        public AreaManager AreaManager { get; }
        public GameObjectManager GameObjectManager { get; }
        public RemotePlayerManager RemotePlayerManager { get; }

        public uint InstanceId { get; private set; }
        public string WorldAreaId { get; private set; }
        public string League { get; private set; }
        public uint Seed { get; private set; }

        public TimeSpan NextUpdateTime { get; private set; }

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
        }

        public void Update()
        {
            RemotePlayerManager.Update();

            // TODO: quantum time calculations
            NextUpdateTime = Clock.UnixTime + TimeSpan.FromMilliseconds(TargetFrameTimeMS);
            AreaManager.EnqueueAreaToUpdate(this);
        }
    }
}
