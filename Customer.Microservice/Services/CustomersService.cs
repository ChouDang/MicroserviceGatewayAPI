using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Customer.Microservice.Services
{
    using Customer.Microservice.Models;
    public class CustomersService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        public CustomersService(IOptions<CustomerDatabaseSettings> CustomerDatabaseSettings)
        {
            var mongoClient = new MongoClient(CustomerDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(CustomerDatabaseSettings.Value.DatabaseName);
            _customerCollection = mongoDatabase.GetCollection<Customer>(CustomerDatabaseSettings.Value.CollectionName);
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> customer = await _customerCollection.Find(_ => true).ToListAsync();
            return customer;
        }
        public async Task<Customer?> GetCustomersAsync(string id) => await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreaterCustomer(Customer newCustomer) => await _customerCollection.InsertOneAsync(newCustomer);
        public async Task UpdateCustomer(string id, Customer newCustomer) => await _customerCollection.ReplaceOneAsync(x => x.Id == id, newCustomer);
        public async Task DeleteCustomer(string id) => await _customerCollection.DeleteOneAsync(id);
    }
}
