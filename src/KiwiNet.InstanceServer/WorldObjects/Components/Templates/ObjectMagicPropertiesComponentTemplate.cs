using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ObjectMagicPropertiesComponentTemplate : ComponentTemplate<ObjectMagicPropertiesComponent>
    {
        public ObjectMagicPropertiesComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ObjectMagicPropertiesComponentTemplateFactory : ComponentTemplateFactory
    {
        public ObjectMagicPropertiesComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ObjectMagicPropertiesComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "ObjectMagicProperties";
        }
    }
}
