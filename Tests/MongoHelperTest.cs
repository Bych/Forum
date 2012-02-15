using Forum.Helpers;
using MongoDB.Driver;

namespace Tests
{
    public class MongoHelperTest : IMongoHelper
    {
        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder("server=localhost;database=ForumTest");
            var server = MongoServer.Create(connectionStringBuilder.ConnectionString);
            var database = server.GetDatabase(connectionStringBuilder.DatabaseName);

            return database.GetCollection<T>(collectionName);
        }

    }
}
