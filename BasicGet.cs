using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace ConsoleApplication1 {
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
            //第四步：获取消息
            var result = channel.BasicGet ("mytest", true);
            var msg = Encoding.UTF8.GetString (result.Body);
        }
    }
}