namespace KiwiNet.InstanceServer.GameObjects
{
    // temp name, used for components that have serialize update methods
    public abstract class ComponentA : Component
    {
        public abstract void Serialize(Stream stream);

        //public abstract void Deserialize(Stream stream);

        //public abstract void SerializeUpdate(Stream stream);

        //public abstract void DeserializeUpdate(Stream stream);
    }
}
