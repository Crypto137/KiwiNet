using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceAllocatePassiveSkillPointPacket : Packet
    {
        public uint PassiveSkill { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            PassiveSkill = connection.Read<uint>();
        }
    }
}
