using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceSkillTargetEntity : Packet
    {
        // seems to mirror SkillTargetLocation, but with target id instead of grid position
        public uint Field0 { get; set; }    // target id?
        public short Field1 { get; set; }   // skill id?
        public short Field2 { get; set; }   // count?
        public byte Field3 { get; set; }    // flags?

        public override string ToString()
        {
            return $"Field0={Field0}, Field1={Field1}, Field2={Field2}, Field3={Field3}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Field1 = connection.Read<short>();
            Field2 = connection.Read<short>();
            Field3 = connection.Read<byte>();
        }
    }
}
