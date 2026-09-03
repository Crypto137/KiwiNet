using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    // temp name, used for components that have serialize update methods
    public abstract class ComponentA : Component
    {
        public abstract void Serialize(NetworkConnection connection);

        //public abstract void Deserialize(NetworkConnection connection);

        //public abstract void SerializeUpdate(NetworkConnection connection);

        //public abstract void DeserializeUpdate(NetworkConnection connection);
    }
}
