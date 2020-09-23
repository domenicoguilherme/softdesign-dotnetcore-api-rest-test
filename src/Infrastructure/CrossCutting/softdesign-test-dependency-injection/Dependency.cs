using Microsoft.Extensions.DependencyInjection;
using softdesign_test_domain.Factories;
using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Interfaces.Services;
using softdesign_test_domain.Services;
using softdesign_test_repository;
using System;

namespace softdesign_test_dependency_injection
{
    public static class Dependency
    {
        public static void RegisterAll(IServiceCollection services, string mongoDbDatabase, string mongoDbConnectionString)
        {
            RegisterMongoDb(services, mongoDbDatabase, mongoDbConnectionString);
            RegisterDomain(services);
        }

        public static void RegisterDomain(IServiceCollection services)
        {
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IApplicationService, ApplicationService>();
        }

        public static void RegisterMongoDb(IServiceCollection services, string mongoDbDatabase, string mongoDbConnectionString)
        {
            if (string.IsNullOrEmpty(mongoDbDatabase))
            {
                throw new ArgumentNullException("mongoDbDatabase", "MongoDB database parameter is required");
            }

            if (string.IsNullOrEmpty(mongoDbConnectionString))
            {
                throw new ArgumentNullException("mongoDbConnectionString", "MongoDb connection string parameter is required");
            }

            services.AddSingleton(new MongoDbFactory(mongoDbConnectionString, mongoDbDatabase).CreateInstance());
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IApplicationService, ApplicationService>();
        }
    }
}