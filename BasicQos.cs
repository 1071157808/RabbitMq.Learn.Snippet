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
                HostName = "192.168.23.149",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            var properties = channel.CreateBasicProperties ();
            //第一个参数prefetchSize是预读取长度，0是不限制
            //第二个参数是每次处理的数量
            //第三个参数是这个设置是否是全局设置
            channel.BasicQos (0, 1, false);
            //properties.Persistent = true;   //消息持久化
            for (int i = 0; i < int.MaxValue; i++) {
                var msg = string.Format ("{0} 你好", string.Join (",", Enumerable.Range (0, 100000)));
                channel.BasicPublish (string.Empty, "mytest1", properties, Encoding.UTF8.GetBytes (msg));
                Console.WriteLine ("{0} 执行完毕", i);
            }
        }
    }
}