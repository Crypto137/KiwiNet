using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PlayerComponentTemplate : ComponentTemplate<PlayerComponent>
    {
        public PlayerComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class PlayerComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new PlayerComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Player";
        }
    }
}
