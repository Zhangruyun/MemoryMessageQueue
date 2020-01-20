using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MemoryQueue
{
    /// <summary>
    /// 消息实际处理者
    /// </summary>
    internal abstract class MessageQueueHandler
    {
        /// <summary>
        /// 返回一个值，该值指示当前对列名称是否绑定消费者
        /// </summary>
        public abstract bool IsBindReceivedEvent { get; }

        /// <summary>
        /// 绑定消费者，并启动线程等待消息发布
        /// </summary>
        /// <param name="received"></param>
        /// <param name="serviceFactory"></param>
        public abstract void BindReceivedEvent(Action<MessageArgument, ServiceFactory> received, ServiceFactory serviceFactory);

        /// <summary>
        /// 返回一个值，该值指示当前消息发布是否成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract bool Publish(MessageArgument message);
    }

    internal class MessageQueueHandlerImpl<TMessage> : MessageQueueHandler where TMessage : MessageArgument
    {
        private BlockingCollection<TMessage> _queue;
        private Action<MessageArgument, ServiceFactory> _received;

        public MessageQueueHandlerImpl()
        {
            _queue = new BlockingCollection<TMessage>();
        }

        public override bool IsBindReceivedEvent
        {
            get => this._received != null;
        }

        [DebuggerStepThrough]
        public override void BindReceivedEvent(Action<MessageArgument, ServiceFactory> received, ServiceFactory serviceFactory)
        {
            this._received = received;
            if (_received != null)
            {
                Task.Factory.StartNew(() =>
                {
                    while (!_queue.IsCompleted)
                    {
                        if (_queue.TryTake(out TMessage args))
                            _received(args, serviceFactory);
                    }
                });
            }
        }

        public override bool Publish(MessageArgument message)
        {
            _queue.Add((TMessage)message);
            return true;
        }
    }
}
