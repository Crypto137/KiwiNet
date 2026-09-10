using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class FlaskTemplate : ComponentTemplate<Flask>
    {
        public FlaskTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class FlaskTemplateFactory : ComponentTemplateFactory
    {
        public FlaskTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new FlaskTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Flask);
        }
    }
}
