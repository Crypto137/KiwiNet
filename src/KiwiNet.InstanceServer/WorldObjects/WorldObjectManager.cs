using KiwiNet.Core.Extensions;
using KiwiNet.Core.Logging;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public class WorldObjectManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private int _sleepLock;
        private readonly Dictionary<uint, WorldObject> _awakeObjects = new();
        private readonly Dictionary<uint, WorldObject> _sleepingObjects = new();
        private readonly List<IWorldObjectEventSubscriber> _subscribers = new();
        private readonly List<WorldObject> _objectsPendingSleep = new();

        private uint _currentId = 0;

        public WorldObjectManager()
        {
        }

        public Dictionary<uint, WorldObject>.ValueCollection.Enumerator GetEnumerator()
        {
            return _awakeObjects.Values.GetEnumerator();
        }

        public WorldObject GetObject(uint id)
        {
            if (_awakeObjects.TryGetValue(id, out WorldObject worldObject) == false)
            {
                if (_sleepingObjects.TryGetValue(id, out worldObject) == false)
                    return null;
            }

            return worldObject;
        }

        public void AddObject(WorldObject worldObject)
        {
            worldObject.Id = ++_currentId;

            _sleepingObjects.Add(worldObject.Id, worldObject);

            foreach (IWorldObjectEventSubscriber subscriber in _subscribers)
                subscriber.OnAddObject(worldObject);

            Logger.Trace($"AddObject(): {worldObject}");
        }

        public void RemoveObject(uint id)
        {
            // Objects needs to be put to sleep before they are removed. If sleep is currently locked,
            // object will be deleted when ProcessObjectsPendingSleep() is called.

            _sleepingObjects.TryGetValue(id, out WorldObject worldObject);

            foreach (IWorldObjectEventSubscriber subscriber in _subscribers)
                subscriber.OnRemoveObject(worldObject);

            _sleepingObjects.Remove(id);

            Logger.Trace($"RemoveObject(): {worldObject}");
        }

        public void WakeObject(uint id)
        {
            if (_sleepingObjects.Remove(id, out WorldObject worldObject))
            {
                _awakeObjects.Add(id, worldObject);

                // TODO: wake components?

                foreach (IWorldObjectEventSubscriber subscriber in _subscribers)
                    subscriber.OnWakeObject(worldObject);
            }
            else
            {
                // Event if the object isn't fully sleeping yet, it may still be pending for sleep.
                int pendingIndex = -1;
                for (int i = 0; i < _objectsPendingSleep.Count; i++)
                {
                    if (_objectsPendingSleep[i].Id == id)
                    {
                        pendingIndex = i;
                        break;
                    }
                }

                if (pendingIndex == -1)
                    throw new Exception("Tried to wake an object that wasn't asleep");

                _objectsPendingSleep.SwapRemove(pendingIndex);
            }
        }

        private void SleepObjectInternal(WorldObject worldObject)
        {
            _awakeObjects.Remove(worldObject.Id);
            _sleepingObjects.Add(worldObject.Id, worldObject);

            _sleepLock++;
            OnSleepObject(worldObject);
            _sleepLock--;
        }

        private void OnSleepObject(WorldObject worldObject)
        {
            foreach (IWorldObjectEventSubscriber subscriber in _subscribers)
                subscriber.OnSleepObject(worldObject);

            // TODO: sleep components?
        }

        public void SleepObject(uint id)
        {
            if (_awakeObjects.TryGetValue(id, out WorldObject worldObject) == false)
                throw new Exception("Trying to sleep an objet that isn't awake");

            if (_objectsPendingSleep.Contains(worldObject))
                return;

            if (_sleepLock > 0)
                _objectsPendingSleep.Add(worldObject);
            else
                SleepObjectInternal(worldObject);
        }

        public void ProcessObjectsPendingSleep()
        {
            if (_sleepLock != 0)
                return;

            List<WorldObject> destroyList = new();

            foreach (WorldObject worldObject in _objectsPendingSleep)
            {
                SleepObjectInternal(worldObject);
                if (worldObject.Destroyed)
                    destroyList.Add(worldObject);
            }

            _objectsPendingSleep.Clear();

            foreach (WorldObject worldObject in destroyList)
            {
                worldObject.Destroy();
                // the client calls delete here, we can return to a pool instead?
            }
        }

        public void AddSubscriber(IWorldObjectEventSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(IWorldObjectEventSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }
    }
}
