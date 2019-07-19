using MongoDB.Bson;
using MongoDB.Driver;
using MyStore.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Mongo.Repository
{
    public class ProductMongoRepository : IProductMongoRepository
    {
        IMongoDBContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMongoRepository"/> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        public ProductMongoRepository(IMongoDBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>prduct collection</returns>
        public async Task<IEnumerable<ProductMongoModel>> GetAllProducts()
        {
            return await _dbContext.Products.AsQueryable().ToListAsync();
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the specific product
        /// </returns>
        public async Task<ProductMongoModel> GetProductById(string id)
        {
            FilterDefinition<ProductMongoModel> filter = Builders<ProductMongoModel>
                .Filter.Eq(x => x.Id, new ObjectId(id));

            return await _dbContext.Products.Find(filter).FirstOrDefaultAsync();
            
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public async Task AddProduct(ProductMongoModel product)
        {
            await _dbContext.Products.InsertOneAsync(product);
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>
        /// returns true if updates successful
        /// </returns>
        public async Task<bool> Update(ProductMongoModel product)
        {
            ReplaceOneResult updatedResult = await _dbContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<ProductMongoModel> filter = Builders<ProductMongoModel>
                .Filter.Eq(x => x.Id, new ObjectId(id));
            DeleteResult deleteResult = await _dbContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
