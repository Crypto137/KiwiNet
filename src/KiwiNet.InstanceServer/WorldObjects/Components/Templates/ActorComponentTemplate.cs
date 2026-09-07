using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ActorComponentTemplate : ComponentTemplate<ActorComponent>
    {
        public ActorComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ActorComponentTemplateFactory : ComponentTemplateFactory
    {
        public ActorComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

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
