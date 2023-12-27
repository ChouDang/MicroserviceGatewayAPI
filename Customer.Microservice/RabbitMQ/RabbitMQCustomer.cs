using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Customer.Microservice.RabbitMQ
{
    public class RabbitMQCustomer : IRabbitMQCustomer
    {
        public void SendCustomerMess<T>(T message) 
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using
                var channel = connection.CreateModel();

            channel.QueueDeclare("customerExchange", exclusive: false);

            var bodyMess = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "customerExchange", routingKey: "customer", body: bodyMess);
            //Console.ReadKey();
        }
    }
}
