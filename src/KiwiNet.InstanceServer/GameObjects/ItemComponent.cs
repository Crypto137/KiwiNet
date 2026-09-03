using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class ItemComponent : Component
    {
        public virtual void Serialize(NetworkConnection connection) { }

        public virtual void Deserialize(NetworkConnection connection) { }
    }
}
