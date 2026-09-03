using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.World
{
    public sealed class WorldItemComponent : WorldComponent
    {
        public ItemObject Item { get; set; }
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
