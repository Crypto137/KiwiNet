using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ModsTemplate : ComponentTemplate<Mods>
    {
        public ModsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ModsTemplateFactory : ComponentTemplateFactory
    {
        public ModsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ModsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Mods);
        }
    }
}
