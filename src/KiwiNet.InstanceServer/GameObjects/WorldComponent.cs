using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class WorldComponent : Component
    {
        public virtual void Serialize(NetworkConnection connection) { }

        public virtual void Deserialize(NetworkConnection connection) { }

        public virtual void SerializeUpdate(NetworkConnection connection) { }

        public virtual void DeserializeUpdate(NetworkConnection connection) { }
    }
}
