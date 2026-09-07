using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items
{
    public abstract class ItemComponent : Component
    {
        public virtual void Serialize(NetworkConnection connection) { }

        public virtual void Deserialize(NetworkConnection connection) { }
    }
}
