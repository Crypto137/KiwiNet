using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QualityTemplate : ComponentTemplate<Quality>
    {
        public QualityTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class QualityTemplateFactory : ComponentTemplateFactory
    {
        public QualityTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new QualityTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Quality);
        }
    }
}
