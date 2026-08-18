using KiwiNet.Core.Config;

namespace KiwiNet.LoginServer
{
    public sealed class LoginServerConfig : IConfig
    {
        public string BindIP { get; private set; } = "0.0.0.0";
        public int Port { get; private set; } = 20481;
        public string InstanceServer { get; private set; } = "localhost";
        public int InstanceServerPort { get; private set; } = 6112;
        public bool LogPackets { get; private set; } = false;
    }
}
