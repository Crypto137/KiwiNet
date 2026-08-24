namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientDoNPCChat : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }

        public InstanceClientDoNPCChat(PacketId id) : base(id)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteByte(stream, Field1);
        }
    }
}
