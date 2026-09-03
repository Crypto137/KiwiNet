using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class WorldComponent : Component
    {
        public abstract void Serialize(NetworkConnection connection);

        //public abstract void Deserialize(NetworkConnection connection);

        //public abstract void SerializeUpdate(NetworkConnection connection);

        //public abstract void DeserializeUpdate(NetworkConnection connection);
    }
}
