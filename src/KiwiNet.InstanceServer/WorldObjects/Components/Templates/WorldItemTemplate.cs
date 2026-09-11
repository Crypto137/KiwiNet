using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class WorldItemTemplate : ComponentTemplate<WorldItem>
    {
        public WorldItemTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Animated));
            dependencies.Add(nameof(Positioned));
        }
    }

    public sealed class WorldItemTemplateFactory : ComponentTemplateFactory
    {
        public WorldItemTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new WorldItemTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(WorldItem);
        }
    }
}
