using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
namespace ConsoleApplication5 {
    class Program {
        static void Main (string[] args) {
            ConnectionFactory factory = new ConnectionFactory () {
                HostName = "192.168.23.148",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            channel.QueueDeclare ("mytest", false, false, false, new Dictionary<string, object> {
                //{"x-message-ttl", 1000*8 }   //queue中的所有message只能存活 8s。。。。
                //{"x-expires", 1000 * 8}
                { "x-max-length", 10 }
            });
            //var properties = channel.CreateBasicProperties();
            //properties.Expiration = "8000";
            for (int i = 0; i < 15; i++) {
                channel.BasicPublish (string.Empty, "mytest", null, Encoding.UTF8.GetBytes (string.Format ("你好 {0}", i)));
            }
            Console.WriteLine ("发布完成");
            Console.Read ();
        }
    }
}