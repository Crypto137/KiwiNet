using KiwiNet.InstanceServer.Areas;

namespace KiwiNet.InstanceServer.GameObjects
{
    public class GameObjectManager
    {
        public Area Area { get; }

        public GameObjectManager(Area area)
        {
            Area = area;
        }
    }
}
