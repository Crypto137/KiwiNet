using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetLocation : Packet
    {
        public uint GridPositionX { get; set; }
        public uint GridPositionY { get; set; }
        public ushort SkillId { get; set; }
        public short Count { get; set; }
        public SkillTargetFlags Flags { get; set; }

        public override string ToString()
        {
            return $"GridPositionX={GridPositionX}, GridPositionY={GridPositionY}, SkillId=0x{SkillId:X}, Count={Count}, Flags={Flags}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            GridPositionX = connection.Read<uint>();
            GridPositionY = connection.Read<uint>();
            SkillId = connection.Read<ushort>();
            Count = connection.Read<short>();
            Flags = (SkillTargetFlags)connection.Read<byte>();
        }
    }
}
