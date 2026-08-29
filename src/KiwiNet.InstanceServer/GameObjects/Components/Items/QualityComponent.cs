using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class QualityComponent : ComponentB
    {
        public int QualityPct { get; set; }

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteInt32(stream, QualityPct);
        }

        public override void Deserialize(Stream stream)
        {
            QualityPct = PacketIO.ReadInt32(stream);
        }
    }
}
