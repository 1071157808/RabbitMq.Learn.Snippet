using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace HelloWorldSend {
    class Program {
        static void Main (string[] args) {
            var factory = new ConnectionFactory () { HostName = "localhost" };
            using (var connection = factory.CreateConnection ())
            using (var channel = connection.CreateModel ()) {
                channel.QueueDeclare (queue: "hello",
                    //durable 永久，持续
                    durable : false,
                    //独有的，排外的
                    exclusive : false,
                    //自动删除
                    autoDelete : false,
                    arguments : null);
                var consumer = new EventingBasicConsumer (channel);
                consumer.Received += (model, ea) => {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString (body);
                    Console.WriteLine (" [x] Received {0}", message);
                };
                channel.BasicConsume (queue: "hello",
                    noAck : true,
                    consumer : consumer);
                Console.WriteLine (" Press [enter] to exit.");
                Console.ReadLine ();
            }
        }
    }
}