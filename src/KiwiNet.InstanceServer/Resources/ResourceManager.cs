namespace KiwiNet.InstanceServer.Resources
{
    public sealed class ResourceManager
    {
        // The client appears to store resources in a linked list? It's easier for us to use a dictionary for now.
        private readonly Dictionary<string, ResourceHandle> _resources = new(StringComparer.OrdinalIgnoreCase);

        public static ResourceManager Instance { get; } = new();

        private ResourceManager() { }

        public ResourceHandle<T> GetResource<T>(string filePath) where T : IResource, new()
        {
            if (_resources.TryGetValue(filePath, out ResourceHandle resource) == false)
                return null;

            return (ResourceHandle<T>)resource;
        }

        public void AddResource(ResourceHandle resource)
        {
            _resources.Add(resource.FileName, resource);
        }

        public void RemoveResource(ResourceHandle resource)
        {
            _resources.Remove(resource.FileName);
            resource.Free();
        }

        public static ResourceHandle<T> Get<T>(string filePath) where T : IResource, new()
        {
            ResourceHandle<T> resource = Instance.GetResource<T>(filePath);

            if (resource == null)
            {
                resource = new ResourceHandle<T>(filePath, Instance);
                Instance.AddResource(resource);
            }

            resource.IncrementRefCount();
            return resource;
        }
    }
}
