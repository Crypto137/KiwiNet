using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class WeaponComponentTemplate : ComponentTemplate<WeaponComponent>
    {
        public WeaponComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class WeaponComponentTemplateFactory : ComponentTemplateFactory
    {
        public WeaponComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new WeaponComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Weapon";
        }
    }
}
