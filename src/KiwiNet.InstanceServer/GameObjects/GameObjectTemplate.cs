using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Resources;
using System.Text;

namespace KiwiNet.InstanceServer.GameObjects
{
    public class GameObjectTemplate
    {
        public string FileName { get; }
        public uint Hash { get; }
        public List<ComponentTemplate> Components { get; } = new();
        public Dictionary<string, int> ComponentIndicesByName { get; } = new(8, StringComparer.OrdinalIgnoreCase);
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

            ComponentIndicesByName[name] = Components.Count;
            Components.Add(component);
        }

        public ComponentTemplate GetComponent(string name)
        {
            if (ComponentIndicesByName.TryGetValue(name, out int index) == false || index == -1)
                return null;

            return Components[index];
        }

        private void LoadComponentTemplatesInternal(string fileName, string fileExtension,
            ComponentTemplateRegistry componentTemplateRegistry, ComponentTemplateParseParams @params)
        {
            string filePath = $"{fileName}.{fileExtension}";

            // the client gets a file from GGPK and uses std::wistream to read it here
            using StreamReader reader = new(filePath, Encoding.Unicode);

            ParseComponentTemplate(reader, fileName, fileExtension, componentTemplateRegistry, @params);
        }

        private void ParseComponentTemplate(StreamReader reader, string fileName, string fileExtension,
            ComponentTemplateRegistry componentTemplateRegistry, ComponentTemplateParseParams @params)
        {
            StringBuilder sb = new();

            // Check version
            if (ParseUtility.MatchToken(reader, "version") == false)
                throw new ResourceException("Error: Expected version");

            if (ParseUtility.GetInt(reader, out int version) == false || version != 2)
                throw new ResourceException("Error: Wrong version number");

            // Check parent template
            if (ParseUtility.MatchToken(reader, "extends") == false)
                throw new ResourceException("Error: Expected extends");

            if (ParseUtility.ExtractString(reader, out string superclass) == false)
                throw new ResourceException("Error: Could not extract superclass");

            if (string.Equals(superclass, "nothing", StringComparison.Ordinal) == false)
                LoadComponentTemplatesInternal(superclass, fileExtension, componentTemplateRegistry, @params);

            for (int i = 0; i < NumCommonComponents; i++)
                PopulateResourceComponent(componentTemplateRegistry, i, fileName);

            // Components
            while (true)
            {
                ParseUtility.GetNextToken(reader, out string componentName);

                // Reading the token will skip whitespace until the end of file if there are no more tokens left
                if (reader.Peek() == -1)
                    break;

                // Check if we have an existing component with this name
                ComponentTemplate component = null;
                if (ComponentIndicesByName.TryGetValue(componentName, out int index))
                    component = Components[index];

                // Allocate a new component if needed
                if (component == null)
                {
                    (component, componentName) = AllocateComponentTemplate(componentName, componentTemplateRegistry);
                    if (component == null)
                        throw new ResourceException($"Error: Component {componentName} does not exist");

                    AddComponent(component, componentName);

                    // Check dependencies
                    List<string> dependencies = new();
                    component.GetDependencies(dependencies);
                    foreach (string dependency in dependencies)
                    {
                        if (ComponentIndicesByName.ContainsKey(dependency) == false)
                            throw new ResourceException($"Error: Dependency {dependency} of {componentName} not satisfied");
                    }

                    // Populate from resource data
                    PopulateResourceComponent(componentTemplateRegistry, Components.Count - 1, fileName);
                }

                // Begin reading component variables
                if (ParseUtility.MatchToken(reader, "{") == false)
                    throw new ResourceException("Error: Expected {");

                while (true)
                {
                    // Check for component end
                    ParseUtility.GetNextToken(reader, out string token);
                    if (string.Equals(token, "}", StringComparison.Ordinal))
                        break;

                    string variableName = token;

                    // Read variable value
                    if (ParseUtility.MatchToken(reader, "=") == false)
                        throw new ResourceException($"Error: Expected an '=' after variable {variableName}");

                    ParseUtility.GetNextToken(reader, out string value);
                    if (string.IsNullOrEmpty(value))
                        throw new ResourceException($"Error: Unexpected end of file");

                    // Determine variable type and set it
                    if (value[0] == ParseUtility.StringLiteralDelimiter)
                    {
                        // String type
                        value = value.TrimStart(ParseUtility.StringLiteralDelimiter);

                        while (true)
                        {
                            // Append tokens until we get one ending with the string literal delimiter
                            if (value[^1] == ParseUtility.StringLiteralDelimiter)
                                break;

                            ParseUtility.GetNextToken(reader, out string append);
                            if (string.IsNullOrEmpty(append))
                                throw new ResourceException("Error: Unexpected end of file in string literal");

                            value = string.Join(' ', value, append);
                        }

                        value = value.TrimEnd(ParseUtility.StringLiteralDelimiter);

                        if (component.SetStringVariable(variableName, value) == false)
                            throw new ResourceException($"Error: Variable {variableName} does not exist or is not of type string");
                    }
                    else if (value == "true" || value == "True") // yes, the client does this, really
                    {
                        // bool true
                        if (component.SetBoolVariable(variableName, true) == false)
                            throw new ResourceException($"Error: Variable {variableName} does not exist or is not of type bool");
                    }
                    else if (value == "false" || value == "False")
                    {
                        // bool false
                        if (component.SetBoolVariable(variableName, false) == false)
                            throw new ResourceException($"Error: Variable {variableName} does not exist or is not of type bool");
                    }
                    else if (value.Contains('.'))
                    {
                        // float
                        if (float.TryParse(value, out float floatValue) == false)
                            throw new ResourceException("Error: Unknown literal type");

                        if (component.SetFloatVariable(variableName, floatValue) == false)
                            throw new ResourceException($"Error: Variable {variableName} does not exist or is not of type float");
                    }
                    else
                    {
                        // int
                        if (int.TryParse(value, out int intValue) == false)
                            throw new ResourceException("Error: Unknown literal type");

                        if (component.SetIntVariable(variableName, intValue) == false)
                            throw new ResourceException($"Error: Variable {variableName} does not exist or is not of type int");
                    }
                }
            }
        }

        private void PopulateResourceComponent(ComponentTemplateRegistry componentTemplateRegistry, int index, string fileName)
        {
            ComponentTemplate component = Components[index];
            
            string name = string.Empty;
            foreach (var kvp in ComponentIndicesByName)
            {
                if (kvp.Value == index)
                {
                    name = kvp.Key;
                    break;
                }
            }

            componentTemplateRegistry.Definitions[name].Factory.PopulateFromResource(component, fileName);
        }

        private (ComponentTemplate, string) AllocateComponentTemplate(string name, ComponentTemplateRegistry componentTemplateRegistry)
        {
            if (componentTemplateRegistry.Definitions.TryGetValue(name, out ComponentTemplateDefinition definition) == false)
                return (null, name);

            ComponentTemplateFactory factory = definition.Factory;

            return (factory.Allocate(this), factory.GetName());
        }
    }
}
