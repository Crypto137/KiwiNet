using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ObjectMagicPropertiesComponentTemplate : ComponentTemplate<ObjectMagicPropertiesComponent>
    {
        public ObjectMagicPropertiesComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ObjectMagicPropertiesComponentTemplateFactory : ComponentTemplateFactory
    {
        public ObjectMagicPropertiesComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ObjectMagicPropertiesComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "ObjectMagicProperties";
        }
    }
}
