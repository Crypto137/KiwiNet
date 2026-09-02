using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientBoundSkillList : Packet
    {
        public uint[] MouseSkills { get; } = new uint[3];
        public uint[] KeyboardSkills { get; } = new uint[5];

        public InstanceClientBoundSkillList() : base(PacketId.InstanceClientBoundSkillListId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            foreach (uint mouseSkill in MouseSkills)
                connection.Write(mouseSkill);

            foreach (uint keyboardSkill in KeyboardSkills)
                connection.Write(keyboardSkill);
        }
    }
}
