using MemoryQueue;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueueTest
{

    public class TestMessage : MessageArgument
    {
        public string body;

        public TestMessage(string body)
        {
            this.body = body;
        }
    }
    public class TestMessage2 : TestMessage
    {
        public TestMessage2(string body) : base(body)
        {
        }
    }

    public class TestConsumer : IConsumer<TestMessage>
    {
        private readonly ILogger<TestConsumer> logger;

        public TestConsumer(ILogger<TestConsumer> logger)
        {
            this.logger = logger;
        }

        public string QueueName => "TestConsumer";

        public void Handle(TestMessage message, ServiceFactory serviceFactory)
        {
            Thread.Sleep(100);
            logger.LogInformation($"Logger TestConsumer Handle --- {message.body} -- {message.Timestamp} -- {message.Id}");
        }
    }
    public class TestConsumer3 : IConsumer<TestMessage>
    {
        private readonly ILogger<TestConsumer> logger;

        public TestConsumer3(ILogger<TestConsumer> logger)
        {
            this.logger = logger;
        }
        public string QueueName => "TestConsumer3";

        public void Handle(TestMessage message, ServiceFactory service)
        {
            Thread.Sleep(1000);
            var aaa = service.GetService<ILogger<TestConsumer>>();
            logger.LogWarning($"TestConsumer3 Handle --- {message.body} -- {message.Timestamp} -- {message.Id}");
        }
    }


    //public class TestConsumer2 : IConsumer<TestMessage2>
    //{
    //    public string QueueName => "TestConsumer2";
    //    public void Handle(TestMessage2 message, ServiceFactory serviceFactory)
    //    {
    //        Console.WriteLine($"TestConsumer2 Handle --- {message.body} -- {message.Timestamp} -- {message.Id}");
    //    }
    //}
}
