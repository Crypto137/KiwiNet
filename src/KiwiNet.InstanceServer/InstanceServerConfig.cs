using KiwiNet.Core.Config;

namespace KiwiNet.InstanceServer
{
    public sealed class InstanceServerConfig : IConfig
    {
        public string BindIP { get; private set; } = "0.0.0.0";
        public int Port { get; private set; } = 6112;
    }
}
