using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Product.Microservice.Services
{
    using Product.Microservice.Models;
    public class ProductsServices
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductsServices(IOptions<ProductDatabaseSettings> ProductDatabaseSettings)
        {
            var mongoClient = new MongoClient(ProductDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(ProductDatabaseSettings.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>(ProductDatabaseSettings.Value.CollectionName);
        }
        public async Task<List<Product>> GetProductsAsync() => await _productCollection.Find(_ => true).ToListAsync();
        public async Task<Product?> GetProductsAsync(string id) => await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreaterProduct(Product newProduct) => await _productCollection.InsertOneAsync(newProduct);
        public async Task UpdateProduct(string id, Product newProduct) => await _productCollection.ReplaceOneAsync(x => x.Id == id, newProduct);
        public async Task DeleteProduct(string id) => await _productCollection.DeleteOneAsync(id);
    }
}
