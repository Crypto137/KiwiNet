using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ArmourComponentTemplate : ComponentTemplate<ArmourComponent>
    {
        public ArmourComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ArmourComponentTemplateFactory : ComponentTemplateFactory
    {
        public ArmourComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ArmourComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Armour";
        }
    }
}
