using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LimitedLifespanTemplate : ComponentTemplate<LimitedLifespan>
    {
        public LimitedLifespanTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class LimitedLifespanTemplateFactory : ComponentTemplateFactory
    {
        public LimitedLifespanTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LimitedLifespanTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(LimitedLifespan);
        }
    }
}
