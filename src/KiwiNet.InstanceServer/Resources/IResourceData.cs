namespace KiwiNet.InstanceServer.Resources
{
    public interface IResourceData
    {
        public void Load(string fileName);

        public void Free();
    }
}
