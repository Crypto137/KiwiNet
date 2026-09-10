using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class StatsComponentTemplate : ComponentTemplate<StatsComponent>
    {
        public StatsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class StatsComponentTemplateFactory : ComponentTemplateFactory
    {
        public StatsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new StatsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Stats";
        }
    }
}
