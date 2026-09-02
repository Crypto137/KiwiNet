using KiwiNet.Core.System;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Network;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace KiwiNet.InstanceServer
{
    // Default precision for thread sleep timing on Windows is 1000/64=15.625 ms.
    // This is not precise enough for our needs, so we request higher resolution timing.
    // According to MS docs, this should be per-process as of Windows 10 2004.
    // https://learn.microsoft.com/en-us/windows/win32/api/timeapi/nf-timeapi-timebeginperiod
    // Also see this for more context:
    // https://randomascii.wordpress.com/2020/10/04/windows-timer-resolution-the-great-rule-change/
    internal static class WinMM
    {
        [SupportedOSPlatform("windows")]
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern int TimeBeginPeriod(int uPeriod);

        [SupportedOSPlatform("windows")]
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern int TimeEndPeriod(int uPeriod);
    }

    public sealed class InstanceServerApp : ServerApp
    {
        public AreaManager AreaManager { get; } = new();
        public ClientSessionManager ClientSessionManager { get; } = new();
        public ClientLobby ClientLobby { get; } = new();
        public InstanceTcpServer TcpServer { get; } = new();

        public static InstanceServerApp Instance { get; } = new();

        private InstanceServerApp() : base("InstanceServer", "KiwiNet.InstanceServer.Config")
        {
        }

        protected override bool InitializeSystems()
        {
            if (OperatingSystem.IsWindows())
                _ = WinMM.TimeBeginPeriod(1);

            AreaManager.Initialize();
            return ClientLobby.Initialize() &&
                   TcpServer.Initialize();
        }

        protected override void DisposeSystems()
        {
            TcpServer.Shutdown();
            ClientLobby.Shutdown();
            AreaManager.Shutdown();

            // Technically this isn't really needed, but MS docs say we should call it.
            if (OperatingSystem.IsWindows())
                _ = WinMM.TimeEndPeriod(1);
        }

        protected override void HandleInput(string input)
        {
        }
    }
}
