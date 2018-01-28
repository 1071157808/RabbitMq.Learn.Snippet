using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace DeadLetter {
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
            channel.QueueDeclare ("mytest", false, false, false, new Dictionary<string, object> { { "x-max-priority", 10 },
            });
            var properties = channel.CreateBasicProperties ();
            for (int i = 0; i < 11; i++) {
                //这样可以实现后进先出，实现一个stack的效果
                properties.Priority = (byte) i;
                channel.BasicPublish (string.Empty, "mytest", null, Encoding.UTF8.GetBytes (string.Format ("你好 {0}", i)));
            }
            Console.WriteLine ("发布完成");
            Console.Read ();
        }
    }
}