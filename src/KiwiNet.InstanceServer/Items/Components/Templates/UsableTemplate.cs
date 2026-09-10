using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class UsableTemplate : ComponentTemplate<Usable>
    {
        public UsableTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class UsableTemplateFactory : ComponentTemplateFactory
    {
        public UsableTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new UsableTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Usable);
        }
    }
}
