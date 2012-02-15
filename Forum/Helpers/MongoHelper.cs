using MongoDB.Driver;
using System.Configuration;

namespace Forum.Helpers
{
    public class MongoHelper : IMongoHelper
    {
        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
            var server = MongoServer.Create(connectionStringBuilder.ConnectionString);
            var database = server.GetDatabase(connectionStringBuilder.DatabaseName);

            return database.GetCollection<T>(collectionName);
        }

    }
}