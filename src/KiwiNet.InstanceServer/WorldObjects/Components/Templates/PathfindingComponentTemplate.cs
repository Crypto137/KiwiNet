using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PathfindingComponentTemplate : ComponentTemplate<PathfindingComponent>
    {
        public PathfindingComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class PathfindingComponentTemplateFactory : ComponentTemplateFactory
    {
        public PathfindingComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new PathfindingComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Pathfinding";
        }
    }
}
