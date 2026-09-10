using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class LocalStatsComponentTemplate : ComponentTemplate<LocalStatsComponent>
    {
        public LocalStatsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class LocalStatsComponentTemplateFactory : ComponentTemplateFactory
    {
        public LocalStatsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LocalStatsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "LocalStats";
        }
    }
}
