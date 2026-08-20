namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceChangeBoundSkill : Packet
    {
        public byte Slot { get; set; }
        public uint Skill { get; set; }

        public ClientInstanceChangeBoundSkill() : base(PacketId.ClientInstanceChangeBoundSkillId)
        {
        }

        public override string ToString()
        {
            return $"Slot={Slot}, Skill=0x{Skill:X8}";
        }

        protected override void DeserializeData(Stream stream)
        {
            Slot = PacketIO.ReadByte(stream);
            Skill = PacketIO.ReadUInt32(stream);
        }
    }
}
