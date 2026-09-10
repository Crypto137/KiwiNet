using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SocketsTemplate : ComponentTemplate<Sockets>
    {
        public SocketsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class SocketsTemplateFactory : ComponentTemplateFactory
    {
        public SocketsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new SocketsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Sockets);
        }
    }
}
