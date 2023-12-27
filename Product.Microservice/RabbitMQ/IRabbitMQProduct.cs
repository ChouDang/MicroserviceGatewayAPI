namespace Product.Microservice.RabbitMQ
{
    public interface IRabbitMQProduct
    {
        public void SendProductMess<T>(T mess);
    }
}
