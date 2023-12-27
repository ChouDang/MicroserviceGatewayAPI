using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Product.Microservice.RabbitMQ
{
    public class RabbitMQProduct : IRabbitMQProduct
    {
        public void SendProductMess<T>(T message) 
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

            channel.QueueDeclare("product", exclusive: false);

            var bodyMess = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "productExchange", routingKey: "product", body: bodyMess);
        }
    }
}
