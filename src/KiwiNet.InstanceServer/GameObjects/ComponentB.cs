using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    // temp name, used for components that don't have separate update serialization functions. this appears to be mostly item components
    public abstract class ComponentB : Component
    {
        public abstract void Serialize(NetworkConnection connection);

        public abstract void Deserialize(NetworkConnection connection);
    }
}
