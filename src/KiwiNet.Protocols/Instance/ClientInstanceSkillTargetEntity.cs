using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetEntity : Packet
    {
        public uint TargetId { get; set; }
        public ushort SkillId { get; set; }
        public short Count { get; set; }
        public byte AttackInPlace { get; set; }

        public override string ToString()
        {
            return $"TargetId={TargetId}, SkillId=0x{SkillId:X}, Count={Count}, AttackInPlace={AttackInPlace}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            TargetId = connection.Read<uint>();
            SkillId = connection.Read<ushort>();
            Count = connection.Read<short>();
            AttackInPlace = connection.Read<byte>();
        }
    }
}
