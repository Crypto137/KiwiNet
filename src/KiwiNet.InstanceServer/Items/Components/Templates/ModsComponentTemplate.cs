using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ModsComponentTemplate : ComponentTemplate<ModsComponent>
    {
        public ModsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ModsComponentTemplateFactory : ComponentTemplateFactory
    {
        public ModsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ModsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Mods";
        }
    }
}
