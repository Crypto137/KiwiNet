namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceSkillTargetEntity : Packet
    {
        // seems to mirror SkillTargetLocation, but with target id instead of grid position
        public uint Field0 { get; set; }    // target id?
        public short Field1 { get; set; }   // skill id?
        public short Field2 { get; set; }   // count?
        public byte Field3 { get; set; }    // flags?

        public ClientInstanceSkillTargetEntity() : base(PacketId.ClientInstanceSkillTargetEntityId)
        {
        }

        public override string ToString()
        {
            return $"Field0={Field0}, Field1={Field1}, Field2={Field2}, Field3={Field3}";
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadUInt32(stream);
            Field1 = PacketIO.ReadInt16(stream);
            Field2 = PacketIO.ReadInt16(stream);
            Field3 = PacketIO.ReadByte(stream);
        }
    }
}
