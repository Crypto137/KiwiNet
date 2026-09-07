using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class FlaskComponentTemplate : ComponentTemplate<FlaskComponent>
    {
        public FlaskComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class FlaskComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new FlaskComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Flask";
        }
    }
}
