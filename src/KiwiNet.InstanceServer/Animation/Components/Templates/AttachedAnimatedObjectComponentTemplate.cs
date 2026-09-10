using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AttachedAnimatedObjectComponentTemplate : ComponentTemplate<AttachedAnimatedObjectComponent>
    {
        public AttachedAnimatedObjectComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AttachedAnimatedObjectComponentTemplateFactory : ComponentTemplateFactory
    {
        public AttachedAnimatedObjectComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AttachedAnimatedObjectComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "AttachedAnimatedObject";
        }
    }
}
