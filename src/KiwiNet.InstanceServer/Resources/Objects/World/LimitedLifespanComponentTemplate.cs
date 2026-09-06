using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class LimitedLifespanComponentTemplate : ComponentTemplate<LimitedLifespanComponent>
    {
        public LimitedLifespanComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class LimitedLifespanComponentTemplateFactory : ComponentTemplateFactory
    {
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
