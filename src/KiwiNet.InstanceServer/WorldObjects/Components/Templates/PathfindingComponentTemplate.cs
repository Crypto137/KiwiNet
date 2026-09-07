using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PathfindingComponentTemplate : ComponentTemplate<PathfindingComponent>
    {
        public PathfindingComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class PathfindingComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new PathfindingComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Pathfinding";
        }
    }
}
