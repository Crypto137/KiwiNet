using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class QualityComponent : ComponentB
    {
        public int QualityPct { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(QualityPct);
        }

        public override void Deserialize(NetworkConnection connection)
        {
            QualityPct = connection.Read<int>();
        }
    }
}
