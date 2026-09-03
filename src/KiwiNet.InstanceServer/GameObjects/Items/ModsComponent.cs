using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Items
{
    public enum Rarity : byte
    {
        Normal,
        Magic,
        Rare,
        Unique,
    }

    public sealed class ModsComponent : ItemComponent
    {
        public Rarity Rarity { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(0);
            connection.Write(0);
            connection.Write(0);
            connection.Write(0);

            connection.Write((byte)Rarity);
            connection.Write(0);
            connection.Write(0);
        }
    }
}
