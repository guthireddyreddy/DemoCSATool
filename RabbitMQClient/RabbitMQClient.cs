using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;

namespace RabbitMQClientLibrary
{
    public class RabbitMQClient
    {
        public void RabbitMQConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "xxx.xx.x.x", UserName = "MyTest", Password = "MyTest", Port = 5672 };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "MyTest", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                        DoSomething(message);
                    };

                    channel.BasicConsume(queue: "MyTest", autoAck: true, consumer: consumer);
                }
            }
        }
        static void DoSomething(string message)
        {
            File.AppendAllText(@"C:\RMQ.txt", message);
        }
    }
}
