using KiwiNet.Core.Utils;

namespace KiwiNet.InstanceServer.Resources.Objects
{
    public class GameObjectTemplate
    {
        public string FilePath { get; }
        public uint Hash { get; }
        public List<ComponentTemplate> Components { get; } = new();
        public Dictionary<string, int> ComponentIndexByName { get; } = new(8, StringComparer.OrdinalIgnoreCase);
        public int NumComponents { get; private set; }

        public GameObjectTemplate(string filePath)
        {
            FilePath = filePath;
            Hash = HashUtility.MurmurHash2(filePath);
        }

        public void LoadComponentTemplates(string filePath)
        {
            LoadComponentTemplatesInternal(filePath);       // a1+4, a1+32, a1
            NumComponents = Components.Count;   // not updated after the second load call
            
            // not sure what this part does yet, need further research
            //if (false)  // a1+64
            //    LoadComponentTemplatesInternal(filePath); // a1+36, a1+64, a1

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

        private void LoadComponentTemplatesInternal(string filePath)
        {
            // TODO
        }
    }
}
