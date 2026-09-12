using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceLinkItemPacket : Packet
    {
        public int LinkIndex { get; set; }
        public byte InventoryType { get; set; }
        public uint EntryId { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            LinkIndex = connection.Read<int>();
            InventoryType = connection.Read<byte>();
            EntryId = connection.Read<uint>();
        }
    }
}
