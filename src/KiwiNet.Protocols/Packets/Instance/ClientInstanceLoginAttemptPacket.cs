namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLoginAttemptPacket : Packet
    {
        public string CharacterName { get; set; } = string.Empty;
        public uint SessionId { get; set; }

        public ClientInstanceLoginAttemptPacket() : base(PacketId.ClientInstanceLoginAttemptPacketId)
        {
        }

        public override string ToString()
        {
            return $"CharacterName={CharacterName}, SessionId=0x{SessionId:X}";
        }

        protected override void DeserializeData(Stream stream)
        {
            CharacterName = PacketIO.ReadString(stream);
            SessionId = PacketIO.ReadUInt32(stream);
        }
    }
}
