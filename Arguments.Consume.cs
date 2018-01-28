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
                HostName = "192.168.23.147",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            EventingBasicConsumer consumer = new EventingBasicConsumer (channel);
            consumer.Received += (sender, e) => {
                var msg = Encoding.UTF8.GetString (e.Body);
                Console.WriteLine (msg);
            };
            channel.BasicConsume ("mytest", true, consumer);
            Console.WriteLine ("consumer1 端启动完毕！！！");
            Console.Read ();
        }
    }
}