using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Animation;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.WorldObjects;

namespace KiwiNet.InstanceServer.GameObjects
{
    public static class GameObjectSystem
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

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
            _ = ItemComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams.ItemParams.CommonFileExtension = "ot";
            ComponentTemplateParseParams.ItemParams.CommonRegistry = ComponentTemplateRegistry.ItemCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.ItemCommon.Definitions.Count} common item components");
        }

        private static void InitializeServerItemObjectTemplates()
        {
            // not sure if there are any server-specific item components

            ComponentTemplateParseParams.ItemParams.ServerFileExtension = "ots";
            ComponentTemplateParseParams.ItemParams.ServerRegistry = ComponentTemplateRegistry.ItemServer;

            Logger.Trace($"Registered {ComponentTemplateRegistry.ItemServer.Definitions.Count} server item components");
        }

        private static void InitializeCommonWorldObjectTemplates()
        {
            // Init factories, this will also populate WorldComponentTemplateRegistry
            _ = WorldComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams.WorldParams.CommonFileExtension = "ot";
            ComponentTemplateParseParams.WorldParams.CommonRegistry = ComponentTemplateRegistry.WorldCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.WorldCommon.Definitions.Count} common world components");
        }

        private static void InitializeServerWorldObjectTemplates()
        {
            // TODO?: ots file for server-specific components for world objects

            ComponentTemplateParseParams.WorldParams.ServerFileExtension = "ots";
            ComponentTemplateParseParams.WorldParams.ServerRegistry = ComponentTemplateRegistry.WorldServer;

            Logger.Trace($"Registered {ComponentTemplateRegistry.WorldServer.Definitions.Count} server world components");
        }

        private static void InitializeCommonAnimationObjectTemplates()
        {
            // Init factories, this will also populate WorldComponentTemplateRegistry
            _ = AnimationComponentTemplateFactoryCollection.Instance;

            ComponentTemplateParseParams.AnimationParams.CommonFileExtension = "ao";
            ComponentTemplateParseParams.AnimationParams.CommonRegistry = ComponentTemplateRegistry.AnimationCommon;

            Logger.Trace($"Registered {ComponentTemplateRegistry.AnimationCommon.Definitions.Count} common animation components");
        }

        private static void InitializeServerAnimationObjectTemplates()
        {
            // not sure if there are any server-specific animation components

            ComponentTemplateParseParams.AnimationParams.ServerFileExtension = "aos";
            ComponentTemplateParseParams.AnimationParams.ServerRegistry = null;

            Logger.Trace($"Registered {ComponentTemplateRegistry.AnimationServer.Definitions.Count} server animation components");
        }
    }
}
