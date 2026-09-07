using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class UsableComponentTemplate : ComponentTemplate<UsableComponent>
    {
        public UsableComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class UsableComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new UsableComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Usable";
        }
    }
}
