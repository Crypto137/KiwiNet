using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Items;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    public sealed class WorldItem : WorldComponent
    {
        public Item Item { get; set; }
        public bool FlippyAnimationPlayed { get; set; }

        public override void Destroy()
        {
            if (Item != null)
            {
                Item.Destroy();
                Item = null;
            }

            base.Destroy();
        }

        public override void Serialize(NetworkConnection connection)
        {
            SerializeUpdate(connection);
        }

        public override void SerializeUpdate(NetworkConnection connection)
        {
            connection.Write(FlippyAnimationPlayed == false);
            connection.Write(Item);
        }
    }
}
