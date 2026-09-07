using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class SkillGemComponentTemplate : ComponentTemplate<SkillGemComponent>
    {
        public SkillGemComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class SkillGemComponentTemplateFactory : ComponentTemplateFactory
    {
        public SkillGemComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new SkillGemComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "SkillGem";
        }
    }
}
