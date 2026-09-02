using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetLocation : Packet
    {
        public uint GridPositionX { get; set; }
        public uint GridPositionY { get; set; }
        public short Field2 { get; set; }   // must be some kind of action/skill identifier
        public short Count { get; set; }    // gets incremented with every use
        public byte Flags { get; set; }     // 1 when holding shift for attack in place, could be a bool

        public override string ToString()
        {
            return $"GridPositionX={GridPositionX}, GridPositionY={GridPositionY}, Field2=0x{Field2:X}, Count={Count}, Flags={Flags}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            GridPositionX = connection.Read<uint>();
            GridPositionY = connection.Read<uint>();
            Field2 = connection.Read<short>();
            Count = connection.Read<short>();
            Flags = connection.Read<byte>();
        }
    }
}
