using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items
{
    public abstract class ItemComponent : Component
    {
        public virtual void Serialize(NetworkConnection connection) { }

        public virtual void Deserialize(NetworkConnection connection) { }
    }
}
