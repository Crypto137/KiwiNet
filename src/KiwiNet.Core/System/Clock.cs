using System.Diagnostics;

namespace KiwiNet.Core.System
{
    /// <summary>
    /// Provides time functionality.
    /// </summary>
    public static class Clock
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        // Use a base datetime + stopwatch to get more accurate timing and not poll system time on every call
        private static readonly DateTime UtcBase;
        private static readonly Stopwatch UtcStopwatch = new();

        static Clock()
        {
            UtcBase = DateTime.UtcNow;
            UtcStopwatch.Start();
        }

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the current precise date and time, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime UtcNowPrecise { get => UtcBase.Add(UtcStopwatch.Elapsed); }

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the current calendar Unix time (epoch Jan 01 1970 00:00:00 GMT+0000).
        /// </summary>
        public static TimeSpan UnixTime { get => UtcNowPrecise - UnixEpoch; }
    }
}
