using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class StackComponentTemplate : ComponentTemplate<StackComponent>
    {
        public StackComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class StackComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new StackComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Stack";
        }
    }
}
