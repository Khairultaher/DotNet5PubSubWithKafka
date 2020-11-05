using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace OrderService.PubSub
{
    public class ProducherWrapper
    {
        private readonly IProducer<Null, string> producer;
        private readonly ProducerConfig config;
        private static readonly Random random = new Random();
        private readonly string topic;
        public ProducherWrapper(ProducerConfig config, string topic)
        {
            this.topic = topic;
            this.config = config;
            producer = new ProducerBuilder<Null, string>(config).Build();

        }

        public async Task WriteMEssage(string message)
        {
            var dr = await producer.ProduceAsync(topic, new Message<Null, string>
            {
                Key = null,
                Value = message,
            });
            Console.WriteLine($"Kafka => Delivered '{dr.Value}' to '{dr.TopicPartition}'");
            return;
        }
    }
}
