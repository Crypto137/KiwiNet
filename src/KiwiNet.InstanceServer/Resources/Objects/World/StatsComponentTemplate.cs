using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class StatsComponentTemplate : ComponentTemplate<StatsComponent>
    {
        public StatsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class StatsComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new StatsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Stats";
        }
    }
}
