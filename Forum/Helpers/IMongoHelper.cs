using MongoDB.Driver;

namespace Forum.Helpers
{
    public interface IMongoHelper
    {
        MongoCollection<T> GetCollection<T>(string collectionName);
    }
}