using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AreaTransitionComponentTemplate : ComponentTemplate<AreaTransitionComponent>
    {
        public AreaTransitionComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AreaTransitionComponentTemplateFactory : ComponentTemplateFactory
    {
        public AreaTransitionComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AreaTransitionComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "AreaTransition";
        }
    }
}
