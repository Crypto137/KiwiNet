using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class FlaskComponentTemplate : ComponentTemplate<FlaskComponent>
    {
        public FlaskComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class FlaskComponentTemplateFactory : ComponentTemplateFactory
    {
        public FlaskComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new FlaskComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Flask";
        }
    }
}
