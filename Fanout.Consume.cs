using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace ConsoleApplication2 {
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
            channel.ExchangeDeclare ("myfanoutexchange", ExchangeType.Fanout, true, false, null);
            //第四步：创建一个队列(queue)
            channel.QueueDeclare ("myfanoutqueue2", true, false, false, null);
            //将queue绑定到exchange之上。。。。
            channel.QueueBind ("myfanoutqueue2", "myfanoutexchange", string.Empty, null);
            EventingBasicConsumer consumer = new EventingBasicConsumer (channel);
            consumer.Received += (sender, e) => {
                var msg = Encoding.UTF8.GetString (e.Body);
                Console.WriteLine (msg);
            };
            channel.BasicConsume ("myfanoutqueue2", true, consumer);
            Console.WriteLine ("consumer2 端启动完毕！！！");
            Console.Read ();
        }
    }
}