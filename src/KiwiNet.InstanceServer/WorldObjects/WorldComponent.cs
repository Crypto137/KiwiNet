using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public abstract class WorldComponent : Component
    {
        public virtual void Serialize(NetworkConnection connection) { }

        public virtual void Deserialize(NetworkConnection connection) { }

        public virtual void SerializeUpdate(NetworkConnection connection) { }

        public virtual void DeserializeUpdate(NetworkConnection connection) { }

        public virtual void ResetUpdate() { }
    }
}
