using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QualityComponentTemplate : ComponentTemplate<QualityComponent>
    {
        public QualityComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class QualityComponentTemplateFactory : ComponentTemplateFactory
    {
        public QualityComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new QualityComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Quality";
        }
    }
}
