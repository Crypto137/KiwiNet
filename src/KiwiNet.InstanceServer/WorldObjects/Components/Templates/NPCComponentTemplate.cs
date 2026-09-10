using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class NPCComponentTemplate : ComponentTemplate<NPCComponent>
    {
        public NPCComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class NPCComponentTemplateFactory : ComponentTemplateFactory
    {
        public NPCComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new NPCComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "NPC";
        }
    }
}
