using KiwiNet.Core.Network;

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

        public override void Deserialize(NetworkConnection connection)
        {
            Slot = connection.Read<byte>();
            Skill = connection.Read<uint>();
        }
    }
}
