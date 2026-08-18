using System.Collections.Concurrent;
using System.Globalization;

namespace KiwiNet.Core.Logging
{
    internal class LoggerThread
    {
        private readonly BlockingCollection<LogMessage> _messages = new();

        private readonly Thread _thread;

        public static LoggerThread Instance { get; } = new();

        private LoggerThread()
        {
            _thread = new(Run)
            {
                Name = "Logging",
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture
            };

            _thread.Start();
        }

        public void AddMessage(in LogMessage logMessage)
        {
            _messages.Add(logMessage);
        }

        private void Run()
        {
            while (true)
            {
                LogMessage message = _messages.Take();
                OutputToConsole(message);
                // TODO: other outputs
            }
        }

        private void OutputToConsole(in LogMessage message)
        {
            Console.ForegroundColor = message.Level switch
            {
                LoggingLevel.Trace => ConsoleColor.DarkGray,
                LoggingLevel.Debug => ConsoleColor.Cyan,
                LoggingLevel.Info  => ConsoleColor.White,
                LoggingLevel.Warn  => ConsoleColor.Yellow,
                LoggingLevel.Error => ConsoleColor.Magenta,
                LoggingLevel.Fatal => ConsoleColor.Red,
                _ => throw new NotImplementedException(),
            };

            Console.WriteLine(message.ToString());
            Console.ResetColor();
        }
    }
}