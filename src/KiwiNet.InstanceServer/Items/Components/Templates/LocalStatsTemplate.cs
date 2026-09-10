using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class LocalStatsTemplate : ComponentTemplate<LocalStats>
    {
        public LocalStatsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class LocalStatsTemplateFactory : ComponentTemplateFactory
    {
        public LocalStatsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LocalStatsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(LocalStats);
        }
    }
}
