namespace KiwiNet.Core.Logging
{
    public enum LoggingLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    internal readonly struct LogMessage
    {
        private const string TimeFormat = "yyyy.MM.dd HH:mm:ss.fff";

        public DateTime Timestamp { get; }
        public LoggingLevel Level { get; }
        public string Logger { get; }
        public string Message { get; }

        public LogMessage(LoggingLevel level, string logger, string message)
        {
            Timestamp = LogManager.LogTimeNow;
            Level = level;
            Logger = logger;
            Message = message;
        }

        public override string ToString()
        {
            return $"[{Timestamp.ToString(TimeFormat)}] [{Level,5}] [{Logger}] {Message}";
        }
    }
}
