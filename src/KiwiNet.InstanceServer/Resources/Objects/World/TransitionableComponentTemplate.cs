using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class TransitionableComponentTemplate : ComponentTemplate<TransitionableComponent>
    {
        public TransitionableComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class TransitionableComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new TransitionableComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Transitionable";
        }
    }
}
