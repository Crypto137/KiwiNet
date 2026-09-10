using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PathfindingTemplate : ComponentTemplate<Pathfinding>
    {
        public PathfindingTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class PathfindingTemplateFactory : ComponentTemplateFactory
    {
        public PathfindingTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new PathfindingTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Pathfinding);
        }
    }
}
