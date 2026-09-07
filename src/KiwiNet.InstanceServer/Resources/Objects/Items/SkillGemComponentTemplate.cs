using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
{
    public sealed class SkillGemComponentTemplate : ComponentTemplate<SkillGemComponent>
    {
        public SkillGemComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class SkillGemComponentTemplateFactory : ComponentTemplateFactory
    {
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
