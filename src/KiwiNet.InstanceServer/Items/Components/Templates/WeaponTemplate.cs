using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class WeaponTemplate : ComponentTemplate<Weapon>
    {
        public WeaponTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class WeaponTemplateFactory : ComponentTemplateFactory
    {
        public WeaponTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new WeaponTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Weapon);
        }
    }
}
