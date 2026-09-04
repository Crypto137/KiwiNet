namespace KiwiNet.InstanceServer.Resources
{
    public sealed class ResourceManager
    {
        // The client appears to store resources in a linked list? It's easier for us to use a dictionary for now.
        private readonly Dictionary<string, Resource> _resources = new(StringComparer.OrdinalIgnoreCase);

        public static ResourceManager Instance { get; } = new();

        private ResourceManager() { }

        public Resource<T> GetResource<T>(string filePath) where T : IResourceData, new()
        {
            if (_resources.TryGetValue(filePath, out Resource resource) == false)
                return null;

            return (Resource<T>)resource;
        }

        public void AddResource(Resource resource)
        {
            _resources.Add(resource.FilePath, resource);
        }

        public void RemoveResource(Resource resource)
        {
            _resources.Remove(resource.FilePath);
            resource.Free();
        }

        public static Resource<T> Get<T>(string filePath) where T : IResourceData, new()
        {
            Resource<T> resource = Instance.GetResource<T>(filePath);

            if (resource == null)
            {
                resource = new Resource<T>(filePath, Instance);
                Instance.AddResource(resource);
            }

            resource.IncrementRefCount();
            return resource;
        }
    }
}
