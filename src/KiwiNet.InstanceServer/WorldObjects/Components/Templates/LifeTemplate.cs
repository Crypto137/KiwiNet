using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LifeTemplate : ComponentTemplate<Life>
    {
        public LifeTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Positioned));
            dependencies.Add(nameof(Pathfinding));
            dependencies.Add(nameof(Stats));
        }
    }

    public sealed class LifeTemplateFactory : ComponentTemplateFactory
    {
        public LifeTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LifeTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Life);
        }
    }
}
