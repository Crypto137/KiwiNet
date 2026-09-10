using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SkillGemComponentTemplate : ComponentTemplate<SkillGemComponent>
    {
        public SkillGemComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class SkillGemComponentTemplateFactory : ComponentTemplateFactory
    {
        public SkillGemComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new SkillGemComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "SkillGem";
        }
    }
}
