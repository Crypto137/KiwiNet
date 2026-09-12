using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    [Flags]
    public enum SkillTargetFlags : byte
    {
        None            = 0,
        AttackInPlace   = 1 << 0,
        InventoryOpen   = 1 << 1,
    }

    public sealed class ClientInstanceSkillTargetEntity : Packet
    {
        public uint TargetId { get; set; }
        public ushort SkillId { get; set; }
        public short Count { get; set; }
        public SkillTargetFlags Flags { get; set; }

        public override string ToString()
        {
            return $"TargetId={TargetId}, SkillId=0x{SkillId:X}, Count={Count}, Flags={Flags}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            TargetId = connection.Read<uint>();
            SkillId = connection.Read<ushort>();
            Count = connection.Read<short>();
            Flags = (SkillTargetFlags)connection.Read<byte>();
        }
    }
}
