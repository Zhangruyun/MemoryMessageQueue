namespace MemoryQueue
{
    /// <summary>
    /// 消息对列
    /// </summary>
    public interface IMessageQueue
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        bool Publish<T>(string queueName, T message) where T : MessageArgument;
    }
}
