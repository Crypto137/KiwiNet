namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientContactUpdate : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public string Field2 { get; set; } 
        public uint Field3 { get; set; }
        public byte Field4 { get; set; }

        public InstanceClientContactUpdate() : base(PacketId.InstanceClientContactUpdateId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteByte(stream, Field1);
            PacketIO.WriteString(stream, Field2);
            PacketIO.WriteUInt32(stream, Field3);
            PacketIO.WriteByte(stream, Field4);
        }
    }
}
