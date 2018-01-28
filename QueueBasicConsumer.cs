using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
namespace ConsoleApplication1 {
    class Program {
        static void Main (string[] args) {
            ConnectionFactory factory = new ConnectionFactory () {
                HostName = "192.168.23.149",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            //这个是不建议使用的，官方已经启用，按照net的习惯还是用事件的方式EventConsumer那种方式比较好
            QueueingBasicConsumer consumer = new QueueingBasicConsumer (channel);
            channel.BasicConsume ("mytest", false, consumer);
            while (true) {
                var result = consumer.Queue.Dequeue ();
                Console.WriteLine (result?.Body);
                if (result == null) {
                    Console.WriteLine ("全部传送完了");
                } else {
                    channel.BasicAck (result.DeliveryTag, false);
                }
            }
            Console.Read ();
        }
    }
}