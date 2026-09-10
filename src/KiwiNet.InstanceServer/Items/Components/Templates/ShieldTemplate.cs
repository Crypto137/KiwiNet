using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ShieldTemplate : ComponentTemplate<Shield>
    {
        public ShieldTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ShieldTemplateFactory : ComponentTemplateFactory
    {
        public ShieldTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ShieldTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Shield);
        }
    }
}
