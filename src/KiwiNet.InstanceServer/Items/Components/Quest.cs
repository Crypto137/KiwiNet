using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Quest : ItemComponent
    {
        public int Field0 { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
        }
    }
}
