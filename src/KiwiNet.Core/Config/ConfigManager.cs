using KiwiNet.Core.Extensions;
using System.Reflection;

namespace KiwiNet.Core.Config
{
    public class ConfigManager
    {
        private readonly Dictionary<Type, IConfig> _configs = new();
        private IniFile _baseConfigFile;
        private IniFile _overrideConfigFile;

        public static ConfigManager Instance { get; } = new();

        private ConfigManager() { }

        public bool Initialize(string configName)
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, $"{configName}.ini");
            if (File.Exists(configPath))
                _baseConfigFile = new(configPath);

            string overridePath = Path.Combine(AppContext.BaseDirectory, $"{configName}Override.ini");
            if (File.Exists(overridePath))
                _overrideConfigFile = new(overridePath);
            else
                File.WriteAllText(overridePath, null);

            return true;
        }

        public static T Get<T>() where T : IConfig, new()
        {
            return Instance.GetConfig<T>();
        }

        private T GetConfig<T>() where T: IConfig, new()
        {
            lock (_configs)
            {
                if (_configs.TryGetValue(typeof(T), out IConfig config) == false)
                {
                    config = CreateConfig<T>();
                    _configs.Add(typeof(T), config);
                }

                return (T)config;
            }
        }

        private T CreateConfig<T>() where T: IConfig, new()
        {
            T config = new();

            if (_baseConfigFile != null)
                ApplyIniFile(config, _baseConfigFile);

            if (_overrideConfigFile != null)
                ApplyIniFile(config, _overrideConfigFile);

            return config;
        }

        private static void ApplyIniFile<T>(T config, IniFile iniFile) where T: IConfig, new()
        {
            Type type = typeof(T);

            string section = type.Name.TrimEnd("Config");

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.IsDefined(typeof(ConfigIgnoreAttribute)))
                    continue;

                object value = Type.GetTypeCode(property.PropertyType) switch
                {
                    TypeCode.String => iniFile.GetString(section, property.Name),
                    TypeCode.Boolean => iniFile.GetBool(section, property.Name),
                    TypeCode.Int32 => iniFile.GetInt32(section, property.Name),
                    TypeCode.UInt32 => iniFile.GetUInt32(section, property.Name),
                    TypeCode.Int64 => iniFile.GetInt64(section, property.Name),
                    TypeCode.UInt64 => iniFile.GetUInt64(section, property.Name),
                    TypeCode.Single => iniFile.GetSingle(section, property.Name),
                    _ => throw new NotImplementedException($"Value type {property.PropertyType} is not supported for config files."),
                };

                if (value != null)
                    property.SetValue(config, value);
            }
        }
    }
}
