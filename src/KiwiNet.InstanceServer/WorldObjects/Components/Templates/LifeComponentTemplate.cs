using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LifeComponentTemplate : ComponentTemplate<LifeComponent>
    {
        public LifeComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class LifeComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new LifeComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Life";
        }
    }
}
