using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SocketsComponentTemplate : ComponentTemplate<SocketsComponent>
    {
        public SocketsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class SocketsComponentTemplateFactory : ComponentTemplateFactory
    {
        public SocketsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new SocketsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Sockets";
        }
    }
}
