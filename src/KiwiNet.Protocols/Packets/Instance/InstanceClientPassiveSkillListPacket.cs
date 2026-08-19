namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientPassiveSkillListPacket : Packet
    {
        public List<uint> PassiveSkills { get; } = new();

        public InstanceClientPassiveSkillListPacket() : base(PacketId.InstanceClientPassiveSkillListPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteInt32(stream, PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                PacketIO.WriteUInt32(stream, passiveSkill);
        }
    }
}
