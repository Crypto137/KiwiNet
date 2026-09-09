using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Animation;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;

namespace KiwiNet.InstanceServer.GameObjects
{
    public static class GameObjectSystem
    {
        public const string ItemObjectTableFile = "Data/BaseItemTypes.csv";
        public const string WorldObjectTableFile = "Metadata/objects.csv";

        private static readonly Logger Logger = LogManager.CreateLogger();

        public static ResourceHandle<CSVTable> ItemObjectCSVTable { get; private set; }
        public static ResourceHandle<CSVTable> WorldObjectCSVTable { get; private set; }

        public static bool Initialize()
        {
            // TODO: check relative to the actual instance server directory rather than the current working directory.
            if (File.Exists(ItemObjectTableFile) == false || File.Exists(WorldObjectTableFile) == false)
            {
                Logger.Fatal("Game data not found! Make sure you extracted Data and Metadata directories from Content.ggpk and placed them in the instance server directory.");
                return false;
            }

            InitializeCommonItemObjectTemplates();
            //InitializeServerItemObjectTemplates();

            InitializeCommonWorldObjectTemplates();
            //InitializeServerWorldObjectTemplates();
            
            InitializeCommonAnimationObjectTemplates();
            //InitializeServerAnimationObjectTemplates();

            return true;
        }

        private static void InitializeCommonItemObjectTemplates()
        {
            // Init factories, this will also populate ItemComponentTemplateRegistry
            _ = ItemCommonComponentTemplateFactoryCollection.Instance;

            using ResourceHandle<CSVTable> itemObjectCSVTable = ResourceManager.Get<CSVTable>(ItemObjectTableFile);
            if (itemObjectCSVTable != ItemObjectCSVTable)
            {
                if (ItemObjectCSVTable != null)
                {
                    ItemObjectCSVTable.DecrementRefCount();
                    ItemObjectCSVTable = null;
                }

                ItemObjectCSVTable = itemObjectCSVTable;
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

            using ResourceHandle<CSVTable> worldObjectCSVTable = ResourceManager.Get<CSVTable>(WorldObjectTableFile);
            if (worldObjectCSVTable != WorldObjectCSVTable)
            {
                if (WorldObjectCSVTable != null)
                {
                    WorldObjectCSVTable.DecrementRefCount();
                    WorldObjectCSVTable = null;
                }

                WorldObjectCSVTable = worldObjectCSVTable;
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
