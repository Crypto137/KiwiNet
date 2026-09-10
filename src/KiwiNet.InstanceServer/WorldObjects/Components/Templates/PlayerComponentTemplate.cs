using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PlayerComponentTemplate : ComponentTemplate<PlayerComponent>
    {
        public PlayerComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class PlayerComponentTemplateFactory : ComponentTemplateFactory
    {
        public PlayerComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new PlayerComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Player";
        }
    }
}
