using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MemoryQueue
{
    /// <summary>
    /// 基于内存消息对列
    /// </summary>
    public class MemoryMessageQueue : IMessageQueue
    {
        private readonly ServiceFactory serviceFactory;
        private ConcurrentDictionary<string, MessageQueueHandler> pairs;

        public MemoryMessageQueue(ServiceFactory serviceFactory)
        {
            this.pairs = new ConcurrentDictionary<string, MessageQueueHandler>();
            this.serviceFactory = serviceFactory;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public bool Publish<TMessage>(string queueName, TMessage message) where TMessage : MessageArgument
        {
            // 获取消息实际处理类
            var queue = pairs.GetOrAdd(queueName, (queueName) =>
            {
                return (MessageQueueHandler)Activator.CreateInstance(typeof(MessageQueueHandlerImpl<>).MakeGenericType(message.GetType()));
            });
            // 获取实现消费者
            var consumers = serviceFactory.GetServices<IConsumer<TMessage>>();
            var consumer = consumers.FirstOrDefault(w => w.QueueName == queueName);
            if (consumer == null)
                throw new InvalidOperationException($"无效的对列名称:{queueName}");

            // 绑定消费者
            if (!queue.IsBindReceivedEvent)
                queue.BindReceivedEvent((message, service) => consumer.Handle((TMessage)message, service), serviceFactory);

            return queue.Publish(message);
        }
    }
}
