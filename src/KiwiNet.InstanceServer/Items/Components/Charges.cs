using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Charges : ItemComponent
    {
        public int Count { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Count);
        }
    }
}
