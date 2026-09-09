using KiwiNet.InstanceServer.Areas;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public enum WorldObjectPacketId
    {
        InstanceClientWorldObjectAdd = 100,
        InstanceClientWorldObjectUpdate,
        InstanceClientWorldObjectRemove,
    }

    public class WorldObjectManager
    {
        public Area Area { get; }

        public WorldObjectManager(Area area)
        {
            Area = area;
        }
    }
}
