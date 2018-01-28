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
            //自动确认
            //BasicGetResult result = channel.BasicGet("mytest", true);
            //手动确认
            BasicGetResult result = channel.BasicGet ("mytest", false);
            //直接扔了
            //channel.BasicReject(result.DeliveryTag, true); //false直接扔掉，true扔回rabbit broker
            //批量拒绝
            //channel.BasicNack()
            //手工确认 确认一条
            channel.BasicRecover (true);
            //手工确认
            //只确认一条，后面的参数为true的话，就可以拒绝所有DeliveryTag大于x的消息
            //这个没搞懂
            channel.BasicAck (result.DeliveryTag, false);
            //EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            //consumer.Received += (sender, e) =>
            //{
            //    var msg = Encoding.UTF8.GetString(e.Body);
            //    Console.WriteLine(msg);
            //};
            //channel.BasicConsume("mytest", true, consumer);
            Console.WriteLine ("consumer1 端启动完毕！！！");
            Console.Read ();
        }
    }
}