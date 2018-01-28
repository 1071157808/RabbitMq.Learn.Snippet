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
            try {
                //发布确认
                channel.TxSelect ();
                for (int i = 0; i < 10; i++) {
                    channel.BasicPublish (string.Empty, "mytest", null, Encoding.UTF8.GetBytes (string.Format ("{0} 你好", i)));
                }
                channel.TxCommit ();
            } catch (Exception ex) {
                channel.TxRollback ();
            }
        }
    }
}