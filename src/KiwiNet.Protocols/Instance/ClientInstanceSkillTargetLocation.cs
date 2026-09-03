using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetLocation : Packet
    {
        public uint GridPositionX { get; set; }
        public uint GridPositionY { get; set; }
        public short SkillId { get; set; }
        public short Count { get; set; }
        public byte AttackInPlace { get; set; }

        public override string ToString()
        {
            return $"GridPositionX={GridPositionX}, GridPositionY={GridPositionY}, SkillId=0x{SkillId:X}, Count={Count}, AttackInPlace={AttackInPlace}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            GridPositionX = connection.Read<uint>();
            GridPositionY = connection.Read<uint>();
            SkillId = connection.Read<short>();
            Count = connection.Read<short>();
            AttackInPlace = connection.Read<byte>();
        }
    }
}
