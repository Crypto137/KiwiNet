namespace KiwiNet.InstanceServer.Resources
{
    public interface IResource
    {
        public void Load(string fileName);

        public void Free();
    }
}
