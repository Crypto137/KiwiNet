using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ShieldComponentTemplate : ComponentTemplate<ShieldComponent>
    {
        public ShieldComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ShieldComponentTemplateFactory : ComponentTemplateFactory
    {
        public ShieldComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

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
