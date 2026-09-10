using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class ItemRegistry : ObjectRegistry<ItemTemplate>
    {
    }

    public sealed class ItemTemplate : ObjectTemplate
    {
        public override void Load(string fileName)
        {
            Initialize(fileName);
            LoadComponentTemplates(fileName, ComponentTemplateParseParams.Item);
        }
    }
}
