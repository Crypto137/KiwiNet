using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class ChargesComponent : ItemComponent
    {
        public int Count { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Count);
        }
    }
}
