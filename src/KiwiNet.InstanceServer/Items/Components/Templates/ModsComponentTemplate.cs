using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ModsComponentTemplate : ComponentTemplate<ModsComponent>
    {
        public ModsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ModsComponentTemplateFactory : ComponentTemplateFactory
    {
        public ModsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ModsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Mods";
        }
    }
}
