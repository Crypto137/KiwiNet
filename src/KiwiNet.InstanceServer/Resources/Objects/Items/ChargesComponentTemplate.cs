using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class ChargesComponentTemplate : ComponentTemplate<ChargesComponent>
    {
        public ChargesComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ChargesComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ChargesComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Charges";
        }
    }
}
