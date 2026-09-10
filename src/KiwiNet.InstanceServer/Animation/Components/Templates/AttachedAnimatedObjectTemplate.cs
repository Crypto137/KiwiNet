using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AttachedAnimatedObjectTemplate : ComponentTemplate<AttachedAnimatedObject>
    {
        public AttachedAnimatedObjectTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AttachedAnimatedObjectTemplateFactory : ComponentTemplateFactory
    {
        public AttachedAnimatedObjectTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AttachedAnimatedObjectTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(AttachedAnimatedObject);
        }
    }
}
