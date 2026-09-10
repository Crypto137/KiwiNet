using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class StatsTemplate : ComponentTemplate<Stats>
    {
        public StatsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class StatsTemplateFactory : ComponentTemplateFactory
    {
        public StatsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new StatsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Stats);
        }
    }
}
