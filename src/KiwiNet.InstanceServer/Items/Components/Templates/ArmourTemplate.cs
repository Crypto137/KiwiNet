using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ArmourTemplate : ComponentTemplate<Armour>
    {
        public ArmourTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ArmourTemplateFactory : ComponentTemplateFactory
    {
        public ArmourTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ArmourTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Armour);
        }
    }
}
