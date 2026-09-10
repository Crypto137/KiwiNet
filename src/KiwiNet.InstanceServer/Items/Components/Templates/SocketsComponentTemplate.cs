using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SocketsComponentTemplate : ComponentTemplate<SocketsComponent>
    {
        public SocketsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class SocketsComponentTemplateFactory : ComponentTemplateFactory
    {
        public SocketsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new SocketsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Sockets";
        }
    }
}
