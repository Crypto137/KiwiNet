namespace KiwiNet.Core.Config
{
    /// <summary>
    /// An attribute for ignoring properties when initializing <see cref="IConfig"/> instances.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigIgnoreAttribute : Attribute { }
}
