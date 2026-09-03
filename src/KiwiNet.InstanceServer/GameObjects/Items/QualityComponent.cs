using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Items
{
    public sealed class QualityComponent : ItemComponent
    {
        public int QualityPct { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(QualityPct);
        }
    }
}
