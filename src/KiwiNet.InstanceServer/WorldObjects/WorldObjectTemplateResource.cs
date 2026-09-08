using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObjectTemplateResource : IResource
    {
        public WorldObjectTemplate Template { get; private set; }

        public void Load(string fileName)
        {
            Template = new(fileName);

            // Positioned is always instantiated first before 
            Template.Positioned = new(Template);
            Template.AddComponent(Template.Positioned, "Positioned");

            Template.LoadComponentTemplates(fileName, ComponentTemplateParseParams.World);

            if (Template.ComponentIndicesByName.TryGetValue("BaseEvents", out int baseEventsIndex))
            {
                ComponentTemplate baseEvents = Template.Components[baseEventsIndex];
                // TODO: set bool68 and dword6C on the template from BaseEvents fields
            }
        }

        public void Free()
        {
        }
    }
}
