using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class SocketsComponent : ComponentB
    {
        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(0);    // item count? socketed items?
            connection.Write(0);    // count for vector of bytes, probably socket colors/links?
        }

        public override void Deserialize(NetworkConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
