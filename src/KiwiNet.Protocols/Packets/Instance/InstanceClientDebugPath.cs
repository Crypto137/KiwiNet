using KiwiNet.Core.Math;
using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientDebugPath : Packet
    {
        public List<Vector2Int> Points { get; } = new();

        public InstanceClientDebugPath() : base(PacketId.InstanceClientDebugPathId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Points.Count);
            foreach (Vector2Int point in Points)
            {
                connection.Write(point.X);
                connection.Write(point.Y);
            }
        }
    }
}
