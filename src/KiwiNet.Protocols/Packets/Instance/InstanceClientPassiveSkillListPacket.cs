using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientPassiveSkillListPacket : Packet
    {
        public List<uint> PassiveSkills { get; } = new();

        public InstanceClientPassiveSkillListPacket() : base(PacketId.InstanceClientPassiveSkillListPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                connection.Write(passiveSkill);
        }
    }
}
