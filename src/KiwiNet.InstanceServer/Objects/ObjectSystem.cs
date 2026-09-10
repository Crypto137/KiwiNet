using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Animation;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;

namespace KiwiNet.InstanceServer.Objects
{
    public static class ObjectSystem
    {
        public const string ItemTableFile = "Data/BaseItemTypes.csv";
        public const string WorldObjectTableFile = "Metadata/objects.csv";

        public const string ItemRegistryFile = "Data/BaseItemTypes.csvf";
        public const string WorldObjectRegistryFile = "Metadata/objects.csvf";

        private static readonly Logger Logger = LogManager.CreateLogger();

        // static references to resources that need to be always loaded
        private static ResourceHandle<ItemRegistry> _itemRegistry;
        private static ResourceHandle<WorldObjectRegistry> _worldObjectRegistry;

        public static bool Initialize()
        {
            // TODO: check relative to the actual instance server directory rather than the current working directory.
            if (File.Exists(ItemTableFile) == false || File.Exists(WorldObjectTableFile) == false)
            {
                Logger.Fatal("Game data not found! Make sure you extracted Data and Metadata directories from Content.ggpk and placed them in the instance server directory.");
                return false;
            }

            InitializeCommonItemTemplates();

            InitializeCommonWorldObjectTemplates();
            InitializeServerWorldObjectTemplates();
            
            InitializeCommonAnimationObjectTemplates();

            {
                using ResourceHandle<WorldObjectRegistry> worldObjectRegistry = ResourceManager.Get<WorldObjectRegistry>(WorldObjectRegistryFile);
                SetStaticResource(ref _worldObjectRegistry, worldObjectRegistry);
            }

            {
                using ResourceHandle<ItemRegistry> itemRegistry = ResourceManager.Get<ItemRegistry>(ItemRegistryFile);
                SetStaticResource(ref _itemRegistry, itemRegistry);
            }

            return true;
        }

        private static void InitializeCommonItemTemplates()
        {
            // Init factories, this will also populate ComponentTemplateRegistry.ItemCommon
            _ = ItemCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams parseParams = ComponentTemplateParseParams.Item;

            using ResourceHandle<CsvDataTable> itemTable = ResourceManager.Get<CsvDataTable>(ItemTableFile);
            parseParams.ObjectTable?.DecrementRefCount();
            parseParams.ObjectTable = itemTable;
            parseParams.ObjectTable?.IncrementRefCount();

            parseParams.CommonFileExtension = "ot";
            parseParams.CommonRegistry = ComponentTemplateRegistry.ItemCommon;
        }

        private static void InitializeCommonWorldObjectTemplates()
        {
            // Init factories, this will also populate ComponentTemplateRegistry.WorldCommon
            _ = WorldCommonComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams parseParams = ComponentTemplateParseParams.World;

            using ResourceHandle<CsvDataTable> worldObjectTable = ResourceManager.Get<CsvDataTable>(WorldObjectTableFile);
            parseParams.ObjectTable?.DecrementRefCount();
            parseParams.ObjectTable = worldObjectTable;
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
