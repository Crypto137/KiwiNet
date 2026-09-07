using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class BaseComponentTemplate : ComponentTemplate<BaseComponent>
    {
        public BaseComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class BaseComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new BaseComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Base";
        }
    }
}
