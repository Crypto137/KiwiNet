using KiwiNet.Core.Utils;

namespace KiwiNet.InstanceServer.GameObjects
{
    public class GameObjectTemplate
    {
        public string FileName { get; }
        public uint Hash { get; }
        public List<ComponentTemplate> Components { get; } = new();
        public Dictionary<string, int> ComponentIndexByName { get; } = new(8, StringComparer.OrdinalIgnoreCase);
        public int NumCommonComponents { get; private set; }

        public GameObjectTemplate(string filePath)
        {
            FileName = filePath;
            Hash = HashUtility.MurmurHash2(filePath);
        }

        public void LoadComponentTemplates(string fileName, ComponentTemplateParseParams @params)
        {
            // Load common components (recursive)
            LoadComponentTemplatesInternal(fileName, @params.CommonFileExtension, @params.CommonRegistry, @params);
            NumCommonComponents = Components.Count;

            // Load client/server specific components (recursive)
            if (@params.ServerRegistry != null)
                LoadComponentTemplatesInternal(fileName, @params.ServerFileExtension, @params.ServerRegistry, @params);

            // Post-process
            foreach (ComponentTemplate component in Components)
                component.PostProcess();
        }

        public void AddComponent(ComponentTemplate component, string name)
        {
            if (Components.Count == byte.MaxValue)
                throw new Exception("Too many components in object");

            ComponentIndexByName[name] = Components.Count;
            Components.Add(component);
        }

        public ComponentTemplate GetComponent(string name)
        {
            if (ComponentIndexByName.TryGetValue(name, out int index) == false || index == -1)
                return null;

            return Components[index];
        }

        private void LoadComponentTemplatesInternal(string fileName, string fileExtension,
            ComponentTemplateRegistry componentTemplateRegistry, ComponentTemplateParseParams @params)
        {
            // TODO
            string filePath = $"{fileName}.{fileExtension}";

        }
    }
}
