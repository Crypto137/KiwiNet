namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientBoundSkillList : Packet
    {
        public uint[] MouseSkills { get; } = new uint[3];
        public uint[] KeyboardSkills { get; } = new uint[5];

        public InstanceClientBoundSkillList() : base(PacketId.InstanceClientBoundSkillListId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            for (int i = 0; i < MouseSkills.Length; i++)
                MouseSkills[i] = PacketIO.ReadUInt32(stream);

            for (int i = 0; i < KeyboardSkills.Length; i++)
                KeyboardSkills[i] = PacketIO.ReadUInt32(stream);
        }
    }
}
