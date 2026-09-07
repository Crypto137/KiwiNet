using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LimitedLifespanComponentTemplate : ComponentTemplate<LimitedLifespanComponent>
    {
        public LimitedLifespanComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class LimitedLifespanComponentTemplateFactory : ComponentTemplateFactory
    {
        public LimitedLifespanComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new LimitedLifespanComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "LimitedLifespan";
        }
    }
}
