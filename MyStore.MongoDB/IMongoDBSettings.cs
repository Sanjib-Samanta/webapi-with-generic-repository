namespace MyStore.MongoDB
{
    public interface IMongoDBSettings
    {
        string MongoDBConnectionString { get; set; }
        string MongoDBName { get; set; }
    }
}