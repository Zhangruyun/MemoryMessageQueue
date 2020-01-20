using MemoryQueue;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTest
{
    public class Worker : BackgroundService
    {
        private readonly IMessageQueue _queue;

        public Worker(IMessageQueue queue)
        {
            this._queue = queue;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_queue.Publish("TestConsumer2", new TestMessage2(DateTime.Now.ToString()));
                _queue.Publish("TestConsumer3", new TestMessage(DateTime.Now.ToString()));
                _queue.Publish("TestConsumer", new TestMessage(DateTime.Now.ToString()));
            }
            return Task.CompletedTask;
        }
    }
}