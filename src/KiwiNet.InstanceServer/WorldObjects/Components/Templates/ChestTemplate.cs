using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ChestTemplate : ComponentTemplate<Chest>
    {
        public ChestTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Positioned));
        }
    }

    public sealed class ChestTemplateFactory : ComponentTemplateFactory
    {
        public ChestTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ChestTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Chest);
        }
    }
}
