using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public enum Rarity : byte
    {
        Normal,
        Magic,
        Rare,
        Unique,
    }

    public sealed class ModsComponent : ComponentB
    {
        public Rarity Rarity { get; set; }

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);

            PacketIO.WriteByte(stream, (byte)Rarity);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
        }

        public override void Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
