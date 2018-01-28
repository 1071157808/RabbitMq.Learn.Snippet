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
            //连接中断的时候，会跳入Connection_ConnectionBlocked函数
            connection.ConnectionBlocked += Connection_ConnectionBlocked;
            //连接恢复的时候，会跳入Connection_ConnectionUnblocked函数
            connection.ConnectionUnblocked += Connection_ConnectionUnblocked;
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            var properties = channel.CreateBasicProperties ();
            //properties.Persistent = true;   //消息持久化
            for (int i = 0; i < int.MaxValue; i++) {
                var msg = string.Format ("{0} 你好", string.Join (",", Enumerable.Range (0, 100000)));
                channel.BasicPublish (string.Empty, "mytest1", properties, Encoding.UTF8.GetBytes (msg));
                Console.WriteLine ("{0} 执行完毕", i);
            }
        }
        private static void Connection_ConnectionUnblocked (object sender, EventArgs e) {
            throw new NotImplementedException ();
        }
        private static void Connection_ConnectionBlocked (object sender, RabbitMQ.Client.Events.ConnectionBlockedEventArgs e) {
            throw new NotImplementedException ();
        }
    }
}