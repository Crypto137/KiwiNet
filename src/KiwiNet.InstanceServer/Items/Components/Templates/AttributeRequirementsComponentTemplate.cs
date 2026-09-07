using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class AttributeRequirementsComponentTemplate : ComponentTemplate<AttributeRequirementsComponent>
    {
        public AttributeRequirementsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AttributeRequirementsComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AttributeRequirementsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "AttributeRequirements";
        }
    }
}
