namespace KiwiNet.Core.Network
{
    public class PacketFactory
    {
        // TODO: packet pooling?

        public static T Get<T>() where T : Packet, new()
        {
            T packet = new();
            return packet;
        }

        public static T Get<T>(byte packetId) where T: Packet, new()
        {
            T packet = new() { Id = packetId };
            return packet;
        }
    }
}
