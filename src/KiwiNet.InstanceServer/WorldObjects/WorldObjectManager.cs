using KiwiNet.Core.Logging;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public class WorldObjectManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        // TODO: awake/sleep and all the other stuff
        private readonly Dictionary<uint, WorldObject> _objects = new();

        private uint _currentId = 0;

        public List<IWorldObjectEventSubscriber> Subscribers { get; } = new();

        public WorldObjectManager()
        {
        }

        public Dictionary<uint, WorldObject>.ValueCollection.Enumerator GetEnumerator()
        {
            return _objects.Values.GetEnumerator();
        }

        public WorldObject GetObject(uint id)
        {
            if (_objects.TryGetValue(id, out WorldObject worldObject) == false)
                return null;

            return worldObject;
        }

        public void AddObject(WorldObject worldObject)
        {
            worldObject.Id = ++_currentId;

            _objects.Add(worldObject.Id, worldObject);

            foreach (IWorldObjectEventSubscriber subscriber in Subscribers)
                subscriber.OnObjectAdded(worldObject);

            Logger.Trace($"AddObject(): {worldObject}");
        }

        public void RemoveObject(uint id)
        {
            _objects.TryGetValue(id, out WorldObject worldObject);

            foreach (IWorldObjectEventSubscriber subscriber in Subscribers)
                subscriber.OnObjectRemoved(worldObject);

            _objects.Remove(id);

            Logger.Trace($"RemoveObject(): {worldObject}");
        }
    }
}
