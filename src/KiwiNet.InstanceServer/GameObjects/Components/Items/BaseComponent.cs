using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class BaseComponent : ComponentB
    {
        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)0);
        }

        public override void Deserialize(NetworkConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
