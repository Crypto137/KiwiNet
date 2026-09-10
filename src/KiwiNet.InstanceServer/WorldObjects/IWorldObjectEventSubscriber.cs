namespace KiwiNet.InstanceServer.WorldObjects
{
    public interface IWorldObjectEventSubscriber
    {
        public void OnObjectAdded(WorldObject worldObject);

        public void OnObjectRemoved(WorldObject worldObject);

        //public void OnObjectAwake(WorldObject worldObject);

        // currently unknown fourth method
    }
}
