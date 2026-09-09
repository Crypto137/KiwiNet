using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class ItemObjectTable : GameObjectTable<ItemObjectTemplate>
    {
    }

    public sealed class ItemObjectTemplate : GameObjectTemplate
    {
        public override void Load(string fileName)
        {
            Initialize(fileName);
            LoadComponentTemplates(fileName, ComponentTemplateParseParams.Item);
        }
    }
}
