using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QualityComponentTemplate : ComponentTemplate<QualityComponent>
    {
        public QualityComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class QualityComponentTemplateFactory : ComponentTemplateFactory
    {
        public QualityComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new QualityComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Quality";
        }
    }
}
