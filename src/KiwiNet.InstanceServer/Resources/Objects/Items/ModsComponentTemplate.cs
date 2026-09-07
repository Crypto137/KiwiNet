using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class ModsComponentTemplate : ComponentTemplate<ModsComponent>
    {
        public ModsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ModsComponentTemplateFactory : ComponentTemplateFactory
    {
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
