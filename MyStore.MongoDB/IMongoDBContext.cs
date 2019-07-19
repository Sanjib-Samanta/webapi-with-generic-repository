using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.MongoDB
{
    public interface IMongoDBContext
    {
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        IMongoCollection<ProductMongoModel> Products { get; }
    }
}