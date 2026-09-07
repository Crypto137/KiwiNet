using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObjectTemplateData : IResourceData
    {
        public WorldObjectTemplate Template { get; private set; }

        public void Load(string fileName)
        {
            Template = new(fileName);

            // Positioned is always instantiated first before 
            Template.Positioned = new(Template);
            Template.AddComponent(Template.Positioned, "Positioned");
        }

        public void Free()
        {
        }
    }
}
