using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class ArmourComponentTemplate : ComponentTemplate<ArmourComponent>
    {
        public ArmourComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ArmourComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ArmourComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Armour";
        }
    }
}
