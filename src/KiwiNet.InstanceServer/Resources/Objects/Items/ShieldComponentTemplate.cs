using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class ShieldComponentTemplate : ComponentTemplate<ShieldComponent>
    {
        public ShieldComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ShieldComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ShieldComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Shield";
        }
    }
}
