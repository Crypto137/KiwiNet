using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Animation;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;

namespace KiwiNet.InstanceServer.GameObjects
{
    public static class GameObjectSystem
    {
        public const string ItemObjectCSVTableFile = "Data/BaseItemTypes.csv";
        public const string WorldObjectCSVTableFile = "Metadata/objects.csv";

        public const string ItemObjectTableFile = "Data/BaseItemTypes.csvf";
        public const string WorldObjectTableFile = "Metadata/objects.csvf";

        private static readonly Logger Logger = LogManager.CreateLogger();

        // static references to resources that need to be always loaded
        private static ResourceHandle<CSVTable> _itemObjectCSVTable;
        private static ResourceHandle<CSVTable> _worldObjectCSVTable;

        private static ResourceHandle<ItemObjectTable> _itemObjectTable;
        private static ResourceHandle<WorldObjectTable> _worldObjectTable;

        public static bool Initialize()
        {
            // TODO: check relative to the actual instance server directory rather than the current working directory.
            if (File.Exists(ItemObjectCSVTableFile) == false || File.Exists(WorldObjectCSVTableFile) == false)
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

            {
                using ResourceHandle<WorldObjectTable> worldObjectTable = ResourceManager.Get<WorldObjectTable>(WorldObjectTableFile);
                SetStaticResource(ref _worldObjectTable, worldObjectTable);
            }

            {
                using ResourceHandle<ItemObjectTable> itemObjectTable = ResourceManager.Get<ItemObjectTable>(ItemObjectTableFile);
                SetStaticResource(ref _itemObjectTable, itemObjectTable);
            }

            return true;
        }

        private static void InitializeCommonItemObjectTemplates()
        {
            // Init factories, this will also populate ItemComponentTemplateRegistry
            _ = ItemCommonComponentTemplateFactoryCollection.Instance;

            using ResourceHandle<CSVTable> itemObjectCSVTable = ResourceManager.Get<CSVTable>(ItemObjectCSVTableFile);
            SetStaticResource(ref _itemObjectCSVTable, itemObjectCSVTable);

            ComponentTemplateParseParams.Item.CSVTable = _itemObjectCSVTable.Resource;  // FIXME
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

            using ResourceHandle<CSVTable> worldObjectCSVTable = ResourceManager.Get<CSVTable>(WorldObjectCSVTableFile);
            SetStaticResource(ref _worldObjectCSVTable, worldObjectCSVTable);

            ComponentTemplateParseParams.World.CSVTable = _worldObjectCSVTable.Resource;    // FIXME
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

        private static void SetStaticResource<T>(ref ResourceHandle<T> staticResourceRef, ResourceHandle<T> newResource) where T: IResource, new()
        {
            if (newResource == staticResourceRef)
                return;

            staticResourceRef?.DecrementRefCount();
            staticResourceRef = newResource;
            staticResourceRef?.IncrementRefCount();
        }
    }
}
