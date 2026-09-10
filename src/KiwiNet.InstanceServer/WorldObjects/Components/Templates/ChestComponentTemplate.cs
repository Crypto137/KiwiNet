using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ChestComponentTemplate : ComponentTemplate<ChestComponent>
    {
        public ChestComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ChestComponentTemplateFactory : ComponentTemplateFactory
    {
        public ChestComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ChestComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Chest";
        }
    }
}
