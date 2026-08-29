using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class SocketsComponent : ComponentB
    {
        public override void Serialize(Stream stream)
        {
            PacketIO.WriteInt32(stream, 0); // item count? socketed items?
            PacketIO.WriteInt32(stream, 0); // count for vector of bytes, probably socket colors/links?
        }

        public override void Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
