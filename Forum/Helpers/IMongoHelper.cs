using MongoDB.Driver;

namespace Forum.Helpers
{
    public interface IMongoHelper
    {
        MongoDatabase Database { get; }
        MongoServer Server { get; }
        MongoCollection<T> GetCollection<T>(string collectionName);
    }
}