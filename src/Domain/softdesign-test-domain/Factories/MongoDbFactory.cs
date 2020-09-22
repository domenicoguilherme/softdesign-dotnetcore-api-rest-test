using MongoDB.Driver;

namespace softdesign_test_domain.Factories
{
    public class MongoDbFactory
    {
        protected readonly string _databaseName;
        protected readonly string _connectionString;

        public MongoDbFactory(string connectionString, string databaseName)
        {
            _databaseName = databaseName;
            _connectionString = connectionString;
        }

        public IMongoDatabase CreateInstance()
        {
            MongoClient client = new MongoClient(_connectionString);
            IMongoDatabase mongoDb = client.GetDatabase(_databaseName);
            
            return mongoDb;
        }
    }
}
