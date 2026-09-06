using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
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
