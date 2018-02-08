using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EC.Libraries.RabbitMQ
{
    class Program
    {
        /// <summary>
        /// 消息队列
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            //生产者
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("product", false, false, false, null);

                    string message = "gouzhiguo";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "name", null, body);
                    Console.WriteLine(" set {0}", message);
                }
            }

            //消费者
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare("hello", false, false, false, null);

            //        var consumer = new QueueingBasicConsumer(channel);
            //        channel.BasicConsume("hello", true, consumer);

            //        Console.WriteLine(" waiting for message.");
            //        while (true)
            //        {
            //            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine("Received {0}", message);

            //        }
            //    }
            //}

        }


    }
}
