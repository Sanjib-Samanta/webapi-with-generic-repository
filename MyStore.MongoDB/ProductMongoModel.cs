using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyStore.MongoDB
{
    public class ProductMongoModel
    {
        private string _name;
        private string _description;
        private double? _price;

        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get => _name; set => _name = value; }
        [BsonElement("Description")]
        public string Description { get => _description; set => _description = value; }
        [BsonElement]
        public double? Price { get => _price; set => _price = value; }
    }
}
