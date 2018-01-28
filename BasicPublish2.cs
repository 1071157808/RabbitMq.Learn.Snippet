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
                HostName = "192.168.23.145",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            //第三步：申明交换机【因为rabbitmq已经有了自定义的ampq default exchange】
            //第四步：创建一个队列(queue)
            channel.QueueDeclare ("mytest", true, false, false, null);
            var msg = Encoding.UTF8.GetBytes ("你好");
            //第五步：发布消息
            channel.BasicPublish (string.Empty, routingKey: "mytest", basicProperties : null, body : msg);
            //using。。。。
            //connection.Dispose();
            //channel.Dispose();
        }
    }
}