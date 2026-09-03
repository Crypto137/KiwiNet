using KiwiNet.InstanceServer.Areas;

namespace KiwiNet.InstanceServer.GameObjects
{
    public enum GameObjectPacketId
    {
        InstanceClientWorldObjectAdd = 100,
        InstanceClientWorldObjectUpdate,
        InstanceClientWorldObjectRemove,
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
