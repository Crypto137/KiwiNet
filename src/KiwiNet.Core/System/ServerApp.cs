using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using System.Globalization;

namespace KiwiNet.Core.System
{
    /// <summary>
    /// Base class for ServerApp singletons.
    /// </summary>
    public abstract class ServerApp
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly string _appName;
        private readonly string _configName;

        private bool _isRunning;

        public ServerApp(string appName, string configName)
        {
            _appName = appName;
            _configName = configName;
        }

        public override string ToString()
        {
            return _appName;
        }

        public virtual void Run()
        {
            if (_isRunning)
                throw new InvalidOperationException($"{GetType().Name} is already running.");

            _isRunning = true;

            PrintBanner();

            ConfigManager.Instance.Initialize(_configName);

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Logger.Info($"Starting {_appName}...");

            try
            {
                if (InitializeSystems() == false)
                    return;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return;
            }

            Logger.Info($"{_appName} started");

            while (_isRunning)
            {
                string input = Console.ReadLine();
                HandleInput(input);
            }

            try
            {
                DisposeSystems();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return;
            }
        }

        protected abstract bool InitializeSystems();

        protected abstract void DisposeSystems();

        protected abstract void HandleInput(string input);

        /// <summary>
        /// Handles unhandled exceptions.
        /// </summary>
        private void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;

            if (e.IsTerminating)
            {
                Logger.Fatal($"{GetType().Name} terminating because of unhandled exception:\n{exception}");
                Console.ReadLine();
            }
            else
            {
                Logger.Error($"Caught unhandled exception:\n{exception}");
            }
        }

        private void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"  _  ___          _   _   _      _   ");
            Console.WriteLine(@" | |/ (_)_      _(_) | \ | | ___| |_ ");
            Console.WriteLine(@" | ' /| \ \ /\ / / | |  \| |/ _ \ __|");
            Console.WriteLine(@" | . \| |\ V  V /| | | |\  |  __/ |_ ");
            Console.WriteLine(@" |_|\_\_| \_/\_/ |_| |_| \_|\___|\__|");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
