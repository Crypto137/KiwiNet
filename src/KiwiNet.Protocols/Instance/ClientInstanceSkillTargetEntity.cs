using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetEntity : Packet
    {
        public uint TargetId { get; set; }
        public short SkillId { get; set; }
        public short Count { get; set; }
        public byte AttackInPlace { get; set; }

        public override string ToString()
        {
            return $"TargetId={TargetId}, SkillId=0x{SkillId:X}, Count=0x{Count:X}, AttackInPlace={AttackInPlace}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            TargetId = connection.Read<uint>();
            SkillId = connection.Read<short>();
            Count = connection.Read<short>();
            AttackInPlace = connection.Read<byte>();
        }
    }
}
