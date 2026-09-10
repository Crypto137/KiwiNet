using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PlayerTemplate : ComponentTemplate<Player>
    {
        public PlayerTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class PlayerTemplateFactory : ComponentTemplateFactory
    {
        public PlayerTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new PlayerTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Player);
        }
    }
}
