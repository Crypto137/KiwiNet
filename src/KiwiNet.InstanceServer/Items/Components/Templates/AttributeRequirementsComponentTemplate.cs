using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class AttributeRequirementsComponentTemplate : ComponentTemplate<AttributeRequirementsComponent>
    {
        public AttributeRequirementsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AttributeRequirementsComponentTemplateFactory : ComponentTemplateFactory
    {
        public AttributeRequirementsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AttributeRequirementsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "AttributeRequirements";
        }
    }
}
