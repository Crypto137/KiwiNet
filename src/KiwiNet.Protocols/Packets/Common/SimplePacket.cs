namespace KiwiNet.Protocols.Packets.Common
{
    /// <summary>
    /// Represents a packet with no data outside of its id.
    /// </summary>
    public sealed class SimplePacket : Packet
    {
        public SimplePacket(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
        }

        protected override void SerializeData(Stream stream)
        {
        }
    }
}
