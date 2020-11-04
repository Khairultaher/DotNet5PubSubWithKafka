using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "Test",
                BootstrapServers = "localhost:9092",
                EnableAutoCommit = false
            };
            using var c = new ConsumerBuilder<Ignore, string>(config).Build();
            c.Subscribe("testTopic");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };
            try
            {
                while (true)
                {
                    var cr = c.Consume(cts.Token);
                    Console.WriteLine($"Consumed message '{cr.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                c.Close();
            }
        }
    }
}
