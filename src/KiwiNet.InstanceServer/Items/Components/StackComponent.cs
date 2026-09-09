using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class StackComponent : ItemComponent
    {
        public int Quantity { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Quantity);
        }
    }
}
