using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LimitedLifespanComponentTemplate : ComponentTemplate<LimitedLifespanComponent>
    {
        public LimitedLifespanComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class LimitedLifespanComponentTemplateFactory : ComponentTemplateFactory
    {
        public LimitedLifespanComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LimitedLifespanComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "LimitedLifespan";
        }
    }
}
