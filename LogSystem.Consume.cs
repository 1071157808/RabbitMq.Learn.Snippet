using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace ConsoleApplication1 {
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
            //第三步：申明交换机【因为rabbitmq已经有了自定义的ampq default exchange】
            channel.ExchangeDeclare ("myexchange", ExchangeType.Direct, true, false, null);
            //第四步：创建一个队列(queue)
            channel.QueueDeclare ("log_else", true, false, false, null);
            var arrr = new string[3] { "debug", "info", "warning" };
            //将debug，info，warning 都绑定到“log_else" 队列中。。。
            for (int i = 0; i < arrr.Length; i++) {
                channel.QueueBind ("log_else", "myexchange", arrr[i], null);
            }
            EventingBasicConsumer consumer = new EventingBasicConsumer (channel);
            consumer.Received += (sender, e) => {
                var msg = Encoding.UTF8.GetString (e.Body);
                Console.WriteLine (msg);
            };
            channel.BasicConsume ("log_else", true, consumer);
            Console.Read ();
            ////第四步：获取消息
            //var result = channel.BasicGet("mytest", true);
            //var msg = Encoding.UTF8.GetString(result.Body);
        }
    }
}