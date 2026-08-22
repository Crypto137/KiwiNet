namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceSkillTargetLocation : Packet
    {
        public uint GridPositionX { get; set; }
        public uint GridPositionY { get; set; }
        public short Field2 { get; set; }   // must be some kind of action/skill identifier
        public short Count { get; set; }    // gets incremented with every use
        public byte Flags { get; set; }     // 1 when holding shift for attack in place, could be a bool

        public ClientInstanceSkillTargetLocation() : base(PacketId.ClientInstanceSkillTargetLocationId)
        {
        }

        public override string ToString()
        {
            return $"GridPositionX={GridPositionX}, GridPositionY={GridPositionY}, Field2=0x{Field2:X}, Count={Count}, Flags={Flags}";
        }

        protected override void DeserializeData(Stream stream)
        {
            GridPositionX = PacketIO.ReadUInt32(stream);
            GridPositionY = PacketIO.ReadUInt32(stream);
            Field2 = PacketIO.ReadInt16(stream);
            Count = PacketIO.ReadInt16(stream);
            Flags = PacketIO.ReadByte(stream);
        }
    }
}
