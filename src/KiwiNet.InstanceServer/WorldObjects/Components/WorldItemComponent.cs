using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Items;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    public sealed class WorldItemComponent : WorldComponent
    {
        public Item Item { get; set; }
        public bool FlippyAnimationPlayed { get; set; }

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
