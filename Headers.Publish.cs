using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace ConsoleApplication5 {
    class Program {
        static void Main (string[] args) {
            ConnectionFactory factory = new ConnectionFactory () {
                HostName = "192.168.23.146",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            var properties = channel.CreateBasicProperties ();
            properties.Headers = new Dictionary<string, object> ();
            properties.Headers.Add ("password", "12345");
            properties.Headers.Add ("username", "jack");
            for (int i = 0; i < 100; i++) {
                var msg = Encoding.UTF8.GetBytes (string.Format ("{0} :{1}", i, "你好"));
                //第五步：发布消息
                channel.BasicPublish ("myheadersexchange", routingKey : string.Empty, basicProperties : properties, body : msg);
                Console.WriteLine (i);
            }
        }
    }
}