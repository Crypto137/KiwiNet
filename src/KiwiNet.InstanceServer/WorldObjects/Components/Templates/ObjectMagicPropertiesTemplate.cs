using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ObjectMagicPropertiesTemplate : ComponentTemplate<ObjectMagicProperties>
    {
        public ObjectMagicPropertiesTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Stats));
        }
    }

    public sealed class ObjectMagicPropertiesTemplateFactory : ComponentTemplateFactory
    {
        public ObjectMagicPropertiesTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ObjectMagicPropertiesTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(ObjectMagicProperties);
        }
    }
}
