using MongoDB.Driver;
using System.Configuration;

namespace Forum.Helpers
{
    public class MongoHelper : IMongoHelper
    {
        private readonly MongoDatabase _database;
        private readonly MongoServer _server;

        public MongoHelper(string configConnectionString)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings[configConnectionString].ConnectionString);
            _server = MongoServer.Create(connectionStringBuilder.ConnectionString);
            _database = _server.GetDatabase(connectionStringBuilder.DatabaseName);
        }

        public MongoDatabase Database
        {
            get { return _database; }
        }

        public MongoServer Server
        {
            get { return _server; }
        }

        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

    }
}