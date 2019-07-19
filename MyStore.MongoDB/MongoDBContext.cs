using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.MongoDB
{
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _mongoDb;
        private readonly IMongoClient _mongoClient;

        /// <summary>Initializes a new instance of the <see cref="MongoDBContext"/> class.</summary>
        /// <param name="settings">The mongo db settings.</param>
        public MongoDBContext(IMongoDBSettings settings)
        {
            _mongoClient = new MongoClient(settings.MongoDBConnectionString);
            _mongoDb = _mongoClient.GetDatabase(settings.MongoDBName);

        }

        /// <summary>Gets the products.</summary>
        /// <value>The products collection</value>
        public IMongoCollection<ProductMongoModel> Products => _mongoDb.GetCollection<ProductMongoModel>("products");
    }
}
