using Microsoft.Extensions.DependencyInjection;
using System;

namespace MemoryQueue.Extensions.Microsoft.DependencyInjection
{
    public class MemoryQueueServiceConfiguration
    {
        public Type QueueImplementationType { get; private set; }
        public ServiceLifetime Lifetime { get; private set; }

        public MemoryQueueServiceConfiguration()
        {
            QueueImplementationType = typeof(MemoryMessageQueue);
            Lifetime = ServiceLifetime.Transient;
        }

        public MemoryQueueServiceConfiguration Using<TQueue>() where TQueue : IMessageQueue
        {
            QueueImplementationType = typeof(TQueue);
            return this;
        }

        public MemoryQueueServiceConfiguration AsSingleton()
        {
            Lifetime = ServiceLifetime.Singleton;
            return this;
        }

        public MemoryQueueServiceConfiguration AsScoped()
        {
            Lifetime = ServiceLifetime.Scoped;
            return this;
        }

        public MemoryQueueServiceConfiguration AsTransient()
        {
            Lifetime = ServiceLifetime.Transient;
            return this;
        }
    }
}
