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
            channel.ExchangeDeclare ("mytopicexchange", ExchangeType.Topic, true, false, null);
            //第四步：创建一个队列(queue)
            channel.QueueDeclare ("mytopicqueue", true, false, false, null);
            //将queue绑定到exchange之上。。。。
            channel.QueueBind ("mytopicqueue", "mytopicexchange", "*.com", null);
            EventingBasicConsumer consumer = new EventingBasicConsumer (channel);
            consumer.Received += (sender, e) => {
                var msg = Encoding.UTF8.GetString (e.Body);
                Console.WriteLine (msg);
            };
            channel.BasicConsume ("mytopicqueue", true, consumer);
            Console.WriteLine ("consumer1 端启动完毕！！！");
            Console.Read ();
        }
    }
}