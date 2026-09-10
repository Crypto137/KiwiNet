using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Quality : ItemComponent
    {
        public int QualityPct { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(QualityPct);
        }
    }
}
