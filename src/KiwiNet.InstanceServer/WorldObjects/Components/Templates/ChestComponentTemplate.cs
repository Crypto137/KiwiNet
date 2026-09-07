using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ChestComponentTemplate : ComponentTemplate<ChestComponent>
    {
        public ChestComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class ChestComponentTemplateFactory : ComponentTemplateFactory
    {
        public ChestComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new ChestComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Chest";
        }
    }
}
