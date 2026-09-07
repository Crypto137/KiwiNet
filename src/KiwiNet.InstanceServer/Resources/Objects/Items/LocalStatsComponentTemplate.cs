using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class LocalStatsComponentTemplate : ComponentTemplate<LocalStatsComponent>
    {
        public LocalStatsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class LocalStatsComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new LocalStatsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "LocalStats";
        }
    }
}
