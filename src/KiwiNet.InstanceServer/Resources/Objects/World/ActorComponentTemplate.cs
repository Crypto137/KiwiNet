using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class ActorComponentTemplate : ComponentTemplate<ActorComponent>
    {
        public ActorComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ActorComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ActorComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Actor";
        }
    }
}
