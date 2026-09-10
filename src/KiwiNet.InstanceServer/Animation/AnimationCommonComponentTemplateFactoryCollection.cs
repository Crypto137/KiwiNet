using KiwiNet.InstanceServer.Animation.Components.Templates;
using KiwiNet.InstanceServer.Objects;

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
            AnimationControllerFactory = new AnimationControllerTemplateFactory(registry);

            AttachedAnimatedObjectFactory = new AttachedAnimatedObjectTemplateFactory(registry);

            HullFactory = new HullTemplateFactory(registry);
        }
    }
}
