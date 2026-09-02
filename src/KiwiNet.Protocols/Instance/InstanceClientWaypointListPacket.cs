using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientWaypointListPacket : Packet
    {
        public uint Field0 { get; set; }
        public List<byte> Field1 { get; } = new();  // bitset? each bit appears to correspond to an unlocked waypoint

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1.Count);
            foreach (byte b in Field1)
                connection.Write(b);
        }
    }
}
