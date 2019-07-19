using MongoDB.Driver;
using MyStore.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Mongo.Repository
{
    
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDBContext _dbContext;

        public ProductRepository(IMongoDBContext context)
        {
            _dbContext = context;
        }

        public IMongoCollection<Product> GetAllProducts()
        {
            return _dbContext.Products;
        }
    }
}
