using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AnimatedComponentTemplate : ComponentTemplate<AnimatedComponent>
    {
        public AnimatedComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AnimatedComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AnimatedComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Animated";
        }
    }
}
