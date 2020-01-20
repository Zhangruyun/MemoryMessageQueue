using System;

namespace MemoryQueue
{
    /// <summary>
    /// 消息
    /// </summary>
    public abstract class MessageArgument
    {
        public Guid Id { get; }
        /// <summary>
        /// 时间戳 (UTC 1970-01-01 00:00:00)
        /// </summary>
        public long Timestamp { get; }

        protected MessageArgument()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow.GetTimeStamp();
        }
    }

    internal static class UtilHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp(this DateTime date)
        {
            TimeSpan ts = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}
