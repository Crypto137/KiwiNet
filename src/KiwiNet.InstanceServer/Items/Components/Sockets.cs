using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Sockets : ItemComponent
    {
        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(0);    // item count? socketed items?
            connection.Write(0);    // count for vector of bytes, probably socket colors/links?
        }
    }
}
