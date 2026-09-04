namespace KiwiNet.InstanceServer.Resources
{
    public interface IResourceData
    {
        public void Load(string filePath);

        public void Free();
    }
}
