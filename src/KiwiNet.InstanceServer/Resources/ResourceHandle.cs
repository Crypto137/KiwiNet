namespace KiwiNet.InstanceServer.Resources
{
    /// <summary>
    /// Type-agnostic data for <see cref="ResourceHandle{T}"/>.
    /// </summary>
    public abstract class ResourceHandle : IDisposable
    {
        private readonly string _fileName;
        private int _refCount;
        private readonly ResourceManager _resourceManager;

        public string FileName { get => _fileName; }

        public ResourceHandle(string filePath, ResourceManager resourceManager)
        {
            _fileName = filePath;
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
    /// A ref-counted instance of <see cref="IResource"/> containing loaded game data.
    /// </summary>
    public class ResourceHandle<T> : ResourceHandle where T : IResource, new()
    {
        public T Resource { get; } = new();

        public ResourceHandle(string filePath, ResourceManager resourceManager) : base(filePath, resourceManager)
        {
            Resource.Load(filePath);
        }

        public override void Free()
        {
            Resource.Free();
        }
    }
}
