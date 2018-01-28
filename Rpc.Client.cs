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
                HostName = "192.168.23.146",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            SimpleRpcClient client = new SimpleRpcClient (channel, string.Empty, ExchangeType.Direct, "rpc_queue");
            var bytes = client.Call (Encoding.UTF8.GetBytes ("hello world！！！！"));
            var result = Encoding.UTF8.GetString (bytes);
        }
    }
}