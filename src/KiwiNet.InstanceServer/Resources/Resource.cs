namespace KiwiNet.InstanceServer.Resources
{
    /// <summary>
    /// Type-agnostic data for <see cref="Resource{T}"/>.
    /// </summary>
    public abstract class Resource : IDisposable
    {
        private readonly string _filePath;
        private int _refCount;
        private readonly ResourceManager _resourceManager;

        public string FilePath { get => _filePath; }

        public Resource(string filePath, ResourceManager resourceManager)
        {
            _filePath = filePath;
            _refCount = 0;
            _resourceManager = resourceManager;
        }

        public void Dispose()
        {
            DecrementRefCount();
        }

        public void IncrementRefCount()
        {
            _refCount++;
        }

        public void DecrementRefCount()
        {
            if (_refCount-- == 1)
                _resourceManager.RemoveResource(this);
        }

        public abstract void Free();
    }

    /// <summary>
    /// A ref-counted instance of <see cref="IResourceData"/> containing loaded game data.
    /// </summary>
    public class Resource<T> : Resource where T : IResourceData, new()
    {
        public T Data { get; } = new();

        public Resource(string filePath, ResourceManager resourceManager) : base(filePath, resourceManager)
        {
            Data.Load(filePath);
        }

        public override void Free()
        {
            Data.Free();
        }
    }
}
