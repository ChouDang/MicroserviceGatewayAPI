namespace Customer.Microservice.RabbitMQ
{
    public interface IRabbitMQCustomer
    {
        public void SendCustomerMess<T>(T mess);
    }
}
