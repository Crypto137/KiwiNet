using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items
{
    public class ItemObjectTemplate : GameObjectTemplate
    {
        public override void Load(string fileName)
        {
            Initialize(fileName);
            LoadComponentTemplates(fileName, ComponentTemplateParseParams.Item);
        }
    }
}
