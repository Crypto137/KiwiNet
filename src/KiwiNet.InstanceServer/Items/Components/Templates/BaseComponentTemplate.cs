using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
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
