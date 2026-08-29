namespace KiwiNet.InstanceServer.GameObjects
{
    // temp name, used for components that don't have separate update serialization functions. this appears to be mostly item components
    public abstract class ComponentB : Component
    {
        public abstract void Serialize(Stream stream);

        public abstract void Deserialize(Stream stream);
    }
}
