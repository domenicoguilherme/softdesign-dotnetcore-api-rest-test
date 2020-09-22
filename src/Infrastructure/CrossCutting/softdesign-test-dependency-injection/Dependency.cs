using Microsoft.Extensions.DependencyInjection;
using softdesign_test_domain.Factories;
using softdesign_test_domain.Interfaces.Repositories;
using softdesign_test_domain.Interfaces.Services;
using softdesign_test_domain.Services;
using softdesign_test_repository;

namespace softdesign_test_dependency_injection
{
    public static class Dependency
    {
        public static void Register(IServiceCollection services, string mongoDbDatabase, string mongoDbConnectionString)
        {
            services.AddSingleton(new MongoDbFactory(mongoDbConnectionString, mongoDbDatabase).CreateInstance());
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IApplicationService, ApplicationService>();
        }
    }
}