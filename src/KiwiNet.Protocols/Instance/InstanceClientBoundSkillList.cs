using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientBoundSkillList : Packet
    {
        public uint[] MouseSkills { get; } = new uint[3];
        public uint[] KeyboardSkills { get; } = new uint[5];

        public override void Serialize(NetworkConnection connection)
        {
            foreach (uint mouseSkill in MouseSkills)
                connection.Write(mouseSkill);

            foreach (uint keyboardSkill in KeyboardSkills)
                connection.Write(keyboardSkill);
        }
    }
}
