using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class SocketsComponentTemplate : ComponentTemplate<SocketsComponent>
    {
        public SocketsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class SocketsComponentTemplateFactory : ComponentTemplateFactory
    {
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
