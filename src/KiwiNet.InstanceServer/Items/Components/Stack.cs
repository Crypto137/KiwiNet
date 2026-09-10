using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Stack : ItemComponent
    {
        public int Quantity { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Quantity);
        }
    }
}
