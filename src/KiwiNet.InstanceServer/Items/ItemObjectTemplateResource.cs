using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Items
{
    public class ItemObjectTemplateResource : IResource
    {
        public GameObjectTemplate Template { get; private set; }

        public void Load(string fileName)
        {
            Template = new(fileName);
            Template.LoadComponentTemplates(fileName, ComponentTemplateParseParams.Item);
        }

        public void Free()
        {
        }
    }
}
