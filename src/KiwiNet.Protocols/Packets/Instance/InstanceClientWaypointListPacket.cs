
namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientWaypointListPacket : Packet
    {
        public uint Field0 { get; set; }
        public List<byte> Field1 { get; } = new();  // bitset? each bit appears to correspond to an unlocked waypoint

        public InstanceClientWaypointListPacket() : base(PacketId.InstanceClientWaypointListPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);

            PacketIO.WriteInt32(stream, Field1.Count);
            foreach (byte b in Field1)
                PacketIO.WriteByte(stream, b);
        }
    }
}
