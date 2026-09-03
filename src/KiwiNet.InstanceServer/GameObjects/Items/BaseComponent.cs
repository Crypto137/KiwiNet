using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Items
{
    public sealed class BaseComponent : ItemComponent
    {
        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)0);
        }
    }
}
