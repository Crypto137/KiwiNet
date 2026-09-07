using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
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
