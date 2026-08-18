namespace KiwiNet.Core.Logging
{
    public class Logger
    {
        private readonly string _name;

        public Logger(string name)
        {
            _name = name;
        }

        public void Trace(string message)
        {
            Log(LoggingLevel.Trace, message);
        }

        public void Debug(string message)
        {
            Log(LoggingLevel.Debug, message);
        }

        public void Info(string message)
        {
            Log(LoggingLevel.Info, message);
        }

        public void Warn(string message)
        {
            Log(LoggingLevel.Warn, message);
        }

        public void Error(string message)
        {
            Log(LoggingLevel.Error, message);
        }

        public void Fatal(string message)
        {
            Log(LoggingLevel.Fatal, message);
        }

        private void Log(LoggingLevel level, string message)
        {
            LogMessage logMessage = new(level, _name, message);
            LoggerThread.Instance.AddMessage(logMessage);
        }
    }
}
