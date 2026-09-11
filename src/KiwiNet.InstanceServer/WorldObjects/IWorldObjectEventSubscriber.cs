namespace KiwiNet.InstanceServer.WorldObjects
{
    public interface IWorldObjectEventSubscriber
    {
        public void OnAddObject(WorldObject worldObject);

        public void OnRemoveObject(WorldObject worldObject);

        public void OnWakeObject(WorldObject worldObject);

        public void OnSleepObject(WorldObject worldObject);
    }
}
