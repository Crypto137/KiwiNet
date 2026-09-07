using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
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
