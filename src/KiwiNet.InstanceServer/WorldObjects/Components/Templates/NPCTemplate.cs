using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class NPCTemplate : ComponentTemplate<NPC>
    {
        public NPCTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class NPCTemplateFactory : ComponentTemplateFactory
    {
        public NPCTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new NPCTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(NPC);
        }
    }
}
