using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Animation;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;

namespace KiwiNet.InstanceServer.GameObjects
{
    public static class GameObjectSystem
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public static ResourceHandle<CSVTable> ItemObjectCSVTable { get; private set; }
        public static ResourceHandle<CSVTable> WorldObjectCSVTable { get; private set; }

        public static void Initialize()
        {
            InitializeCommonItemObjectTemplates();
            //InitializeServerItemObjectTemplates();

            InitializeCommonWorldObjectTemplates();
            //InitializeServerWorldObjectTemplates();
            
            InitializeCommonAnimationObjectTemplates();
            //InitializeServerAnimationObjectTemplates();
        }

        private static void InitializeCommonItemObjectTemplates()
        {
            // Init factories, this will also populate ItemComponentTemplateRegistry
            _ = ItemCommonComponentTemplateFactoryCollection.Instance;

            using ResourceHandle<CSVTable> itemObjectTable = ResourceManager.Get<CSVTable>("Data/BaseItemTypes.csv");
            if (itemObjectTable != ItemObjectCSVTable)
            {
                if (ItemObjectCSVTable != null)
                {
                    ItemObjectCSVTable.DecrementRefCount();
                    ItemObjectCSVTable = null;
                }

                ItemObjectCSVTable = itemObjectTable;
                ItemObjectCSVTable.IncrementRefCount();
            }

            ComponentTemplateParseParams.Item.CommonFileExtension = "ot";
            ComponentTemplateParseParams.Item.CommonRegistry = ComponentTemplateRegistry.ItemCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.ItemCommon.Definitions.Count} common item components");
        }

        private static void InitializeServerItemObjectTemplates()
        {
            // not sure if there are any server-specific item components

            ComponentTemplateParseParams.Item.ServerFileExtension = "ots";
            ComponentTemplateParseParams.Item.ServerRegistry = ComponentTemplateRegistry.ItemServer;

            Logger.Trace($"Registered {ComponentTemplateRegistry.ItemServer.Definitions.Count} server item components");
        }

        private static void InitializeCommonWorldObjectTemplates()
        {
            // Init factories, this will also populate WorldComponentTemplateRegistry
            _ = WorldCommonComponentTemplateFactoryCollection.Instance;

            using ResourceHandle<CSVTable> worldObjectTable = ResourceManager.Get<CSVTable>("Metadata/objects.csv");
            if (worldObjectTable != WorldObjectCSVTable)
            {
                if (WorldObjectCSVTable != null)
                {
                    WorldObjectCSVTable.DecrementRefCount();
                    WorldObjectCSVTable = null;
                }

                WorldObjectCSVTable = worldObjectTable;
                WorldObjectCSVTable.IncrementRefCount();
            }

            ComponentTemplateParseParams.World.CommonFileExtension = "ot";
            ComponentTemplateParseParams.World.CommonRegistry = ComponentTemplateRegistry.WorldCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.WorldCommon.Definitions.Count} common world components");
        }

        private static void InitializeServerWorldObjectTemplates()
        {
            // TODO?: ots file for server-specific components for world objects

            ComponentTemplateParseParams.World.ServerFileExtension = "ots";
            ComponentTemplateParseParams.World.ServerRegistry = ComponentTemplateRegistry.WorldServer;

            Logger.Trace($"Registered {ComponentTemplateRegistry.WorldServer.Definitions.Count} server world components");
        }

        private static void InitializeCommonAnimationObjectTemplates()
        {
            // Init factories, this will also populate WorldComponentTemplateRegistry
            _ = AnimationCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams.Animation.CommonFileExtension = "ao";
            ComponentTemplateParseParams.Animation.CommonRegistry = ComponentTemplateRegistry.AnimationCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.AnimationCommon.Definitions.Count} common animation components");
        }

        private static void InitializeServerAnimationObjectTemplates()
        {
            // not sure if there are any server-specific animation components

            ComponentTemplateParseParams.Animation.ServerFileExtension = "aos";
            ComponentTemplateParseParams.Animation.ServerRegistry = null;

            Logger.Trace($"Registered {ComponentTemplateRegistry.AnimationServer.Definitions.Count} server animation components");
        }
    }
}
