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

            InitializeCommonWorldObjectTemplates();
            InitializeServerWorldObjectTemplates();
            
            InitializeCommonAnimationObjectTemplates();

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
            // Init factories, this will also populate ComponentTemplateRegistry.ItemCommon
            _ = ItemCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams parseParams = ComponentTemplateParseParams.Item;

            using ResourceHandle<CSVTable> itemObjectCSVTable = ResourceManager.Get<CSVTable>(ItemObjectCSVTableFile);
            parseParams.ObjectTable?.DecrementRefCount();
            parseParams.ObjectTable = itemObjectCSVTable;
            parseParams.ObjectTable?.IncrementRefCount();

            parseParams.CommonFileExtension = "ot";
            parseParams.CommonRegistry = ComponentTemplateRegistry.ItemCommon;
        }

        private static void InitializeCommonWorldObjectTemplates()
        {
            // Init factories, this will also populate ComponentTemplateRegistry.WorldCommon
            _ = WorldCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams parseParams = ComponentTemplateParseParams.World;

            using ResourceHandle<CSVTable> worldObjectCSVTable = ResourceManager.Get<CSVTable>(WorldObjectCSVTableFile);
            parseParams.ObjectTable?.DecrementRefCount();
            parseParams.ObjectTable = worldObjectCSVTable;
            parseParams.ObjectTable?.IncrementRefCount();

            parseParams.CommonFileExtension = "ot";
            parseParams.CommonRegistry = ComponentTemplateRegistry.WorldCommon;
        }

        private static void InitializeServerWorldObjectTemplates()
        {
            // TODO?: ots file for server-specific components for world objects

            //ComponentTemplateParseParams.World.ServerFileExtension = "ots";
            //ComponentTemplateParseParams.World.ServerRegistry = ComponentTemplateRegistry.WorldServer;
        }

        private static void InitializeCommonAnimationObjectTemplates()
        {
            // Init factories, this will also populate ComponentTemplateRegistry.AnimationCommon
            _ = AnimationCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams parseParams = ComponentTemplateParseParams.Animation;

            parseParams.CommonFileExtension = "ao";
            parseParams.CommonRegistry = ComponentTemplateRegistry.AnimationCommon;
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
