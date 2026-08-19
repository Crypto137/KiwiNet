namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class ClientLoginRequestCreateCharacterPacket : Packet
    {
        public string Name { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        public uint Field2 { get; set; }    // always 0x0?
        public uint Field3 { get; set; }    // same for different sessions and accounts, but changes on client restart
        public CharacterClass Class { get; set; }

        public ClientLoginRequestCreateCharacterPacket() : base(PacketId.ClientLoginRequestCreateCharacterPacketId)
        {
        }

        public override string ToString()
        {
            return $"Name={Name}, League={League}, Field2=0x{Field2:X}, Field3=0x{Field3:X}, Class={Class}";
        }

        protected override void DeserializeData(Stream stream)
        {
            Name = PacketIO.ReadStringUtf16(stream);
            League = PacketIO.ReadStringUtf16(stream);
            Field2 = PacketIO.ReadUInt32(stream);
            Field3 = PacketIO.ReadUInt32(stream);
            Class = (CharacterClass)PacketIO.ReadUInt32(stream);
        }
    }
}
