using System.Diagnostics;

namespace KiwiNet.Core.Logging
{
    public static class LogManager
    {
        private static readonly Dictionary<string, Logger> _loggerDict = new();

        private static readonly DateTime _logTimeBase;
        private static readonly Stopwatch _logTimeStopwatch;

        public static DateTime LogTimeNow { get => _logTimeBase.Add(_logTimeStopwatch.Elapsed); }

        static LogManager()
        {
            // Use a base datetime + stopwatch to get more accurate timing and not poll system time on every log message
            _logTimeBase = DateTime.Now;
            _logTimeStopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Creates or returns existing <see cref="Logger"/> instance with the same name as the caller's <see cref="Type"/>.
        /// </summary>
        public static Logger CreateLogger()
        {
            StackFrame stackFrame = new(1, false);
            string callerName = stackFrame.GetMethod().DeclaringType.Name;
            return callerName == null
                ? throw new Exception("LogManager failed to get caller name when creating a logger.")
                : CreateLogger(callerName);
        }

        /// <summary>
        /// Creates or returns existing <see cref="Logger"/> instance with the specified name.
        /// </summary>
        public static Logger CreateLogger(string name)
        {
            lock (_loggerDict)
            {
                if (_loggerDict.TryGetValue(name, out Logger logger) == false)
                {
                    logger = new(name);
                    _loggerDict.Add(name, logger);
                }

                return logger;
            }
        }

    }
}
