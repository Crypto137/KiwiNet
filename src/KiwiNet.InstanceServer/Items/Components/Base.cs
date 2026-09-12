using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Items.Components.Templates;
using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class Base : ItemComponent
    {
        public BaseTemplate Template { get; private set; }

        public override void Initialize(ComponentTemplate template, ObjectBase owner)
        {
            base.Initialize(template, owner);

            Template = (BaseTemplate)template;
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)0);
        }
    }
}
