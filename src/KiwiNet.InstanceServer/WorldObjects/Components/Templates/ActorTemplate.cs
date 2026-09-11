using KiwiNet.Core.Logging;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public enum ActorSize
    {
        Small,
        Medium,
        Large,
        Epic,
    }

    public enum ItemClass
    {
        LifeFlask = 1,
        ManaFlask,
        HybridFlask,
        Currency,
        Amulet,
        Ring,
        Claw,
        Dagger,
        Wand,
        One_Hand_Sword,
        Thrusting_One_Hand_Sword,
        One_Hand_Axe,
        One_Hand_Mace,
        Bow,
        Staff,
        Two_Hand_Sword,
        Two_Hand_Axe,
        Two_Hand_Mace,
        Active_Skill_Gem,
        Support_Skill_Gem,
        Quiver,
        Belt,
        Gloves,
        Boots,
        Body_Armour,
        Helmet,
        Shield,
        SmallRelic,
        MediumRelic,
        LargeRelic,
        StackableCurrency,
        QuestItem,
        Invalid,
    }

    public sealed class ActorTemplate : ComponentTemplate<Actor>
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public string Actor { get; set; }
        public int Team { get; set; }
        public ActorSize ActorSize { get; set; }
        public ItemClass MainHandUnarmedType { get; set; } = ItemClass.Invalid;
        public ItemClass OffHandUnarmedType { get; set; } = ItemClass.Invalid;
        public List<ushort> BasicActions { get; } = new();

        public ActorTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override bool SetIntVariable(string name, int value)
        {
            switch (name)
            {
                case "team":
                    Team = value;
                    return true;
            }

            return false;
        }

        public override bool SetStringVariable(string name, string value)
        {
            switch (name)
            {
                case "actor":
                    Actor = value;
                    return true;

                case "basic_action":
                    ushort hash = HashUtility.Fnv1a16(value);
                    BasicActions.Add(hash);
                    return true;

                case "actor_size":
                    if (Enum.TryParse(value, out ActorSize actorSize))
                        ActorSize = actorSize;
                    else
                        Logger.Warn($"Unknown value when trying to set actor size, string value '{value}' is invalid. Skipping.");

                    return true;

                case "main_hand_unarmed_type":
                    if (Enum.TryParse(value, out ItemClass itemClass) == false)
                        throw new ResourceException($"Invalid item class {value}");

                    MainHandUnarmedType = itemClass;
                    return true;

                case "off_hand_unarmed_type":
                    if (Enum.TryParse(value, out itemClass) == false)
                        throw new ResourceException($"Invalid item class {value}");

                    OffHandUnarmedType = itemClass;
                    return true;
            }

            return false;
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Pathfinding));
            dependencies.Add(nameof(Stats));
            dependencies.Add(nameof(Life));
            dependencies.Add(nameof(Animated));
        }
    }

    public sealed class ActorTemplateFactory : ComponentTemplateFactory
    {
        public ActorTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ActorTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Actor);
        }
    }
}
