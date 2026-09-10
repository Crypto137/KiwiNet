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
            ArmourFactory = new ArmourComponentTemplateFactory(registry);

            // TODO: load Data/ComponentAttributeRequirements.dat
            AttributeRequirementsFactory = new AttributeRequirementsComponentTemplateFactory(registry);

            // TODO: load Data/BaseItemTypes.dat
            BaseFactory = new BaseComponentTemplateFactory(registry);

            // TODO: load Data/ComponentCharges.dat
            ChargesFactory = new ChargesComponentTemplateFactory(registry);

            ModsFactory = new ModsComponentTemplateFactory(registry);

            QualityFactory = new QualityComponentTemplateFactory(registry);

            // TODO: load Data/ComponentSkillGem.dat
            SkillGemFactory = new SkillGemComponentTemplateFactory(registry);

            SocketsFactory = new SocketsComponentTemplateFactory(registry);

            // TODO: load Data/CurrencyItems.dat
            // TODO: load Data/BaseItemTypes.dat
            StackFactory = new StackComponentTemplateFactory(registry);

            // TODO: load Data/CurrencyItems.dat
            // TODO: load Data/BaseItemTypes.dat
            UsableFactory = new UsableComponentTemplateFactory(registry);

            // TODO: load Data/ComponentWeapon.dat
            WeaponFactory = new WeaponComponentTemplateFactory(registry);

            LocalStatsFactory = new LocalStatsComponentTemplateFactory(registry);

            // TODO: load Data/ComponentFlask.dat
            FlaskFactory = new FlaskComponentTemplateFactory(registry);

            // TODO: load Data/ShieldTypes.dat
            // TODO: load Data/BaseItemTypes.dat
            ShieldFactory = new ShieldComponentTemplateFactory(registry);

            QuestFactory = new QuestComponentTemplateFactory(registry);
        }
    }
}
