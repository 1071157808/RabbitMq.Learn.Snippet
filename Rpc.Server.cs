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
                HostName = "192.168.23.146",
                UserName = "datamip",
                Password = "datamip",
            };
            //第一步：创建connection
            var connection = factory.CreateConnection ();
            //第二步：创建一个channel
            var channel = connection.CreateModel ();
            channel.QueueDeclare ("rpc_queue", true, false, false, null);
            Subscription subscription = new Subscription (channel, "rpc_queue");
            MySimpleRpcServer server = new MySimpleRpcServer (subscription);
            Console.WriteLine ("server 端启动完毕！！！");
            server.MainLoop ();
            Console.Read ();
        }
    }
    public class MySimpleRpcServer : SimpleRpcServer {
        public MySimpleRpcServer (Subscription subscription) : base (subscription) { }
        public override byte[] HandleCall (bool isRedelivered, IBasicProperties requestProperties, byte[] body, out IBasicProperties replyProperties) {
            return base.HandleCall (isRedelivered, requestProperties, body, out replyProperties);
        }
        public override byte[] HandleSimpleCall (bool isRedelivered, IBasicProperties requestProperties, byte[] body, out IBasicProperties replyProperties) {
            replyProperties = null;
            var msg = string.Format ("当前文字长度为：{0}", Encoding.UTF8.GetString (body).Length);
            return Encoding.UTF8.GetBytes (msg);
            //return base.HandleSimpleCall(isRedelivered, requestProperties, body, out replyProperties);
        }
        public override void ProcessRequest (BasicDeliverEventArgs evt) {
            base.ProcessRequest (evt);
        }
    }
}