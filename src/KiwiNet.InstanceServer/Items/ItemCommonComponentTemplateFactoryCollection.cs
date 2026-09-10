using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Items.Components.Templates;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class ItemCommonComponentTemplateFactoryCollection
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

        public static ItemCommonComponentTemplateFactoryCollection Instance { get; } = new();

        private ItemCommonComponentTemplateFactoryCollection()
        {
            ComponentTemplateRegistry registry = ComponentTemplateRegistry.ItemCommon;

            // TODO: load Data/ComponentArmour.dat
            ArmourFactory = new ArmourTemplateFactory(registry);

            // TODO: load Data/ComponentAttributeRequirements.dat
            AttributeRequirementsFactory = new AttributeRequirementsTemplateFactory(registry);

            // TODO: load Data/BaseItemTypes.dat
            BaseFactory = new BaseTemplateFactory(registry);

            // TODO: load Data/ComponentCharges.dat
            ChargesFactory = new ChargesTemplateFactory(registry);

            ModsFactory = new ModsTemplateFactory(registry);

            QualityFactory = new QualityTemplateFactory(registry);

            // TODO: load Data/ComponentSkillGem.dat
            SkillGemFactory = new SkillGemTemplateFactory(registry);

            SocketsFactory = new SocketsTemplateFactory(registry);

            // TODO: load Data/CurrencyItems.dat
            // TODO: load Data/BaseItemTypes.dat
            StackFactory = new StackTemplateFactory(registry);

            // TODO: load Data/CurrencyItems.dat
            // TODO: load Data/BaseItemTypes.dat
            UsableFactory = new UsableTemplateFactory(registry);

            // TODO: load Data/ComponentWeapon.dat
            WeaponFactory = new WeaponTemplateFactory(registry);

            LocalStatsFactory = new LocalStatsTemplateFactory(registry);

            // TODO: load Data/ComponentFlask.dat
            FlaskFactory = new FlaskTemplateFactory(registry);

            // TODO: load Data/ShieldTypes.dat
            // TODO: load Data/BaseItemTypes.dat
            ShieldFactory = new ShieldTemplateFactory(registry);

            QuestFactory = new QuestTemplateFactory(registry);
        }
    }
}
