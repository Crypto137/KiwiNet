using KiwiNet.InstanceServer.Areas;

namespace KiwiNet.InstanceServer.GameObjects
{
    public enum GameObjectPacketId
    {
        InstanceClientObjectAdd = 100,
        InstanceClientObjectUpdate,
        InstanceClientObjectRemove,
    }

    public class GameObjectManager
    {
        public Area Area { get; }

        public GameObjectManager(Area area)
        {
            Area = area;
        }
    }
}
