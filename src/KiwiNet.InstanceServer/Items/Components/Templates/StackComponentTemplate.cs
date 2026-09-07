using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
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
