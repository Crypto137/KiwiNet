using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstancePlaceSocketable : Packet
    {
        public byte InventoryType { get; set; }
        public uint EntryId { get; set; }
        public int SocketIndex { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            InventoryType = connection.Read<byte>();
            EntryId = connection.Read<uint>();
            SocketIndex = connection.Read<int>();
        }
    }
}
