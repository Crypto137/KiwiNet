using KiwiNet.InstanceServer.Animation.Components.Templates;
using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimationCommonComponentTemplateFactoryCollection
    {
        public ComponentTemplateFactory AnimationControllerFactory { get; }
        public ComponentTemplateFactory AttachedAnimatedObjectFactory { get; }
        public ComponentTemplateFactory HullFactory { get; }

        public static AnimationCommonComponentTemplateFactoryCollection Instance { get; } = new();

        private AnimationCommonComponentTemplateFactoryCollection()
        {
            ComponentTemplateRegistry registry = ComponentTemplateRegistry.AnimationCommon;

            // TODO: load amd_table
            AnimationControllerFactory = new AnimationControllerComponentTemplateFactory(registry);

            AttachedAnimatedObjectFactory = new AttachedAnimatedObjectComponentTemplateFactory(registry);

            HullFactory = new HullComponentTemplateFactory(registry);
        }
    }
}
