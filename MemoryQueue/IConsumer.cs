namespace MemoryQueue
{
    /// <summary>
    /// 消费者接口
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IConsumer<in TMessage> where TMessage : MessageArgument
    {
        public string QueueName { get; }
        void Handle(TMessage message, ServiceFactory serviceFactory);
    }
}
