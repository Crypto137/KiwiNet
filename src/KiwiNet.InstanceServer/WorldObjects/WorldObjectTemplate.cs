using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.WorldObjects.Components.Templates;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObjectRegistry : ObjectRegistry<WorldObjectTemplate>
    {
    }

    public sealed class WorldObjectTemplate : ObjectTemplate
    {
        public PositionedComponentTemplate Positioned { get; set; }
        public bool Bool68 { get; set; }
        public int Dword6C { get; set; }

        public override void Load(string fileName)
        {
            Initialize(fileName);

            // Positioned is always instantiated first before 
            Positioned = new(this);
            AddComponent(Positioned, "Positioned");

            LoadComponentTemplates(fileName, ComponentTemplateParseParams.World);

            if (ComponentIndicesByName.TryGetValue("BaseEvents", out int baseEventsIndex))
            {
                ComponentTemplate baseEvents = Components[baseEventsIndex];
                // TODO: set bool68 and dword6C from BaseEvents fields
            }
        }
    }
}
