using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ArmourComponentTemplate : ComponentTemplate<ArmourComponent>
    {
        public ArmourComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ArmourComponentTemplateFactory : ComponentTemplateFactory
    {
        public ArmourComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

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
