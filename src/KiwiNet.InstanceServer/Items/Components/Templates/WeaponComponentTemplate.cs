using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class WeaponComponentTemplate : ComponentTemplate<WeaponComponent>
    {
        public WeaponComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class WeaponComponentTemplateFactory : ComponentTemplateFactory
    {
        public WeaponComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new WeaponComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Weapon";
        }
    }
}
