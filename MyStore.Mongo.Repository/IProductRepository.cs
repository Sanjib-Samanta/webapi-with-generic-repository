using MongoDB.Driver;
using MyStore.MongoDB;

namespace MyStore.Mongo.Repository
{
    public interface IProductRepository
    {
        IMongoCollection<Product> GetAllProducts();
    }
}