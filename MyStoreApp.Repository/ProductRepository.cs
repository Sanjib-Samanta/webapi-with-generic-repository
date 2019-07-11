using MyStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStoreApp.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(Guid productId);
    }

    public class ProductRepository : IProductRepository
    {
        IMyStoreEntities _myStoreDbContext;

        public ProductRepository(IMyStoreEntities dbContext)
        {
            _myStoreDbContext = dbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _myStoreDbContext.Products.ToList();
        }

        public Product GetProduct(Guid productId)
        {
            return _myStoreDbContext.Products.
                Where(p => p.Id.ToString().Equals(productId.ToString()))
                .FirstOrDefault();
        }
        
    }
}
