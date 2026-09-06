using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class ObjectMagicPropertiesComponentTemplate : ComponentTemplate<ObjectMagicPropertiesComponent>
    {
        public ObjectMagicPropertiesComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ObjectMagicPropertiesComponentTemplateFactory : ComponentTemplateFactory
    {
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
