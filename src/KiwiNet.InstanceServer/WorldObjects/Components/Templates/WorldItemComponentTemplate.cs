using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class WorldItemComponentTemplate : ComponentTemplate<WorldItemComponent>
    {
        public WorldItemComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class WorldItemComponentTemplateFactory : ComponentTemplateFactory
    {
        public WorldItemComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new WorldItemComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "WorldItem";
        }
    }
}
