using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class ActorComponentTemplate : ComponentTemplate<ActorComponent>
    {
        public ActorComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ActorComponentTemplateFactory : ComponentTemplateFactory
    {
        public ActorComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ActorComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Actor";
        }
    }
}
