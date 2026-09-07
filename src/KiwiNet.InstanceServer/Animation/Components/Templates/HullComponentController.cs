using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class HullComponentTemplate : ComponentTemplate<HullComponent>
    {
        public HullComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class HullComponentTemplateFactory : ComponentTemplateFactory
    {
        public HullComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new HullComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Hull";
        }
    }
}
