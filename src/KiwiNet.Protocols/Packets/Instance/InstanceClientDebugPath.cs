using KiwiNet.Core.Math;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientDebugPath : Packet
    {
        public List<Vector2Int> Points { get; } = new();

        public InstanceClientDebugPath() : base(PacketId.InstanceClientDebugPathId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteByte(stream, (byte)Points.Count);
            foreach (Vector2Int point in Points)
            {
                PacketIO.WriteInt32(stream, point.X);
                PacketIO.WriteInt32(stream, point.Y);
            }
        }
    }
}
