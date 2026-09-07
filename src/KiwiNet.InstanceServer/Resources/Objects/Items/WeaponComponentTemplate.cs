using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class WeaponComponentTemplate : ComponentTemplate<WeaponComponent>
    {
        public WeaponComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class WeaponComponentTemplateFactory : ComponentTemplateFactory
    {
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
