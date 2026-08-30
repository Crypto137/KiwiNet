namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class ClientLoginCreateAccountPacket : Packet
    {
        public string Field0 { get; set; }              // email or name
        public string Field1 { get; set; }              // email or name
        public byte[] Field2 { get; } = new byte[32];   // password hash

        public ClientLoginCreateAccountPacket() : base(PacketId.ClientLoginCreateAccountPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadString(stream);
            Field1 = PacketIO.ReadString(stream);
            stream.Read(Field2);
        }
    }
}
