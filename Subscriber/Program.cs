using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "ORDER",
                BootstrapServers = "localhost:9092",
                EnableAutoCommit = false
            };
            using var c = new ConsumerBuilder<Ignore, string>(config).Build();
            c.Subscribe("orders");

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
                    Order order = JsonConvert.DeserializeObject<Order>(cr.Message.Value);
                    //Console.WriteLine($"Consumed message '{order.Id}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                    Console.WriteLine($"Kafka => Order '{order.Id}' is accepted from topic '{cr.Topic}'");

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
