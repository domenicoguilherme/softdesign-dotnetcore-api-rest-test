using Microsoft.Extensions.DependencyInjection;
using softdesign_test_dependency_injection;
using System;
using Xunit;

namespace softdesign_test_dependency_injection_test
{
    public class DependencyTest
    {
        [Fact]
        public void SHOULD_VALIDATE_MONGODB_DATABASE()
        {
            var mongoDbDatabase = string.Empty;
            var serviceCollection = new ServiceCollection();
            var mongoDbConnectionString = "mongodb://localhost:27017";

            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                Dependency.RegisterMongoDb(serviceCollection, mongoDbDatabase, mongoDbConnectionString);
            });

            Assert.Equal("MongoDB database parameter is required (Parameter 'mongoDbDatabase')", exception.Message);
        }

        [Fact]
        public void SHOULD_VALIDATE_MONGODB_CONNECTION_STRING()
        {
            var mongoDbDatabase = "ApplicationDb";
            var serviceCollection = new ServiceCollection();
            var mongoDbConnectionString = string.Empty;

            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                Dependency.RegisterMongoDb(serviceCollection, mongoDbDatabase, mongoDbConnectionString);
            });

            Assert.Equal("MongoDb connection string parameter is required (Parameter 'mongoDbConnectionString')", exception.Message);
        }
    }
}
