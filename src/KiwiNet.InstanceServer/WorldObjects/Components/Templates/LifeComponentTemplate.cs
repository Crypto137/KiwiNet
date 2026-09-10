using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class LifeComponentTemplate : ComponentTemplate<LifeComponent>
    {
        public LifeComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class LifeComponentTemplateFactory : ComponentTemplateFactory
    {
        public LifeComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new LifeComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Life";
        }
    }
}
