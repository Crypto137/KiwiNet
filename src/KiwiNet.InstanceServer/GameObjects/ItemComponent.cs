using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class ItemComponent : Component
    {
        public abstract void Serialize(NetworkConnection connection);

        //public abstract void Deserialize(NetworkConnection connection);
    }
}
