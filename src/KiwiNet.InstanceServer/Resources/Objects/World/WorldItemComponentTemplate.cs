using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class WorldItemComponentTemplate : ComponentTemplate<WorldItemComponent>
    {
        public WorldItemComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class WorldItemComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new WorldItemComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "WorldItem";
        }
    }
}
