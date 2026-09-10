using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SkillGemTemplate : ComponentTemplate<SkillGem>
    {
        public SkillGemTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class SkillGemTemplateFactory : ComponentTemplateFactory
    {
        public SkillGemTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new SkillGemTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(SkillGem);
        }
    }
}
