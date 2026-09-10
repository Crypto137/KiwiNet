using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class AttributeRequirementsTemplate : ComponentTemplate<AttributeRequirements>
    {
        public AttributeRequirementsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AttributeRequirementsTemplateFactory : ComponentTemplateFactory
    {
        public AttributeRequirementsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AttributeRequirementsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(AttributeRequirements);
        }
    }
}
