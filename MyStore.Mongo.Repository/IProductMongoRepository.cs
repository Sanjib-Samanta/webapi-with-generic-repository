using MongoDB.Driver;
using MyStore.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Mongo.Repository
{
    public interface IProductMongoRepository
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>product collection</returns>
        Task<IEnumerable<ProductMongoModel>> GetAllProducts();


        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the specific product</returns>
        Task<ProductMongoModel> GetProductById(string id);

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        Task AddProduct(ProductMongoModel product);

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>returns true if updates successful</returns>
        Task<bool> Update(ProductMongoModel product);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> Delete(string id);
    }
}