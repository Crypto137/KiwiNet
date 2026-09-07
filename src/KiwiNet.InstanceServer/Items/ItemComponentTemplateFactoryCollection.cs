using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Items.Components.Templates;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class ItemComponentTemplateFactoryCollection
    {
        public ComponentTemplateFactory ArmourFactory { get; }
        public ComponentTemplateFactory AttributeRequirementsFactory { get; }
        public ComponentTemplateFactory BaseFactory { get; }
        public ComponentTemplateFactory ChargesFactory { get; }
        public ComponentTemplateFactory ModsFactory { get; }
        public ComponentTemplateFactory QualityFactory { get; }
        public ComponentTemplateFactory SkillGemFactory { get; }
        public ComponentTemplateFactory SocketsFactory { get; }
        public ComponentTemplateFactory StackFactory { get; }
        public ComponentTemplateFactory UsableFactory { get; }
        public ComponentTemplateFactory WeaponFactory { get; }
        public ComponentTemplateFactory LocalStatsFactory { get; }
        public ComponentTemplateFactory FlaskFactory { get; }
        public ComponentTemplateFactory ShieldFactory { get; }
        public ComponentTemplateFactory QuestFactory { get; }

        public static ItemComponentTemplateFactoryCollection Instance { get; } = new();

        private ItemComponentTemplateFactoryCollection()
        {
        }
    }
}
