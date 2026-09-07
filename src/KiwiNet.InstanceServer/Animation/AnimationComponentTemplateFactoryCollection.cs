using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimationComponentTemplateFactoryCollection
    {
        public ComponentTemplateFactory AnimationControllerFactory { get; }
        public ComponentTemplateFactory AttachedAnimatedObjectFactory { get; }
        public ComponentTemplateFactory HullFactory { get; }

        public static AnimationComponentTemplateFactoryCollection Instance { get; } = new();

        private AnimationComponentTemplateFactoryCollection()
        {
        }
    }
}
