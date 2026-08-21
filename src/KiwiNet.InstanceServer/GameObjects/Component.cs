namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class Component
    {
        public abstract void Serialize(Stream stream);
    }
}
