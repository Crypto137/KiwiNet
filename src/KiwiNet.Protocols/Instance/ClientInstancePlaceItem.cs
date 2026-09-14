using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstancePlaceItem : Packet
    {
        public byte InventoryType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            InventoryType = connection.Read<byte>();
            X = connection.Read<int>();
            Y = connection.Read<int>();
        }
    }
}
