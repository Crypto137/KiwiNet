namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceRequestWaypointUsePacket : Packet
    {
        public uint Field0 { get; set; }
        public uint Field1 { get; set; }
        public byte Field2 { get; set; }

        public ClientInstanceRequestWaypointUsePacket() : base(PacketId.ClientInstanceRequestWaypointUsePacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadUInt32(stream);
            Field1 = PacketIO.ReadUInt32(stream);
            Field2 = PacketIO.ReadByte(stream);
        }
    }
}
