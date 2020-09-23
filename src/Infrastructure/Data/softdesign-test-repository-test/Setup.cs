using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using softdesign_test_dependency_injection;

namespace softdesign_test_repository_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Dependency.RegisterDomain(services);
            services.AddSingleton(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}