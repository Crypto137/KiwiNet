using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class QualityComponentTemplate : ComponentTemplate<QualityComponent>
    {
        public QualityComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class QualityComponentTemplateFactory : ComponentTemplateFactory
    {
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
