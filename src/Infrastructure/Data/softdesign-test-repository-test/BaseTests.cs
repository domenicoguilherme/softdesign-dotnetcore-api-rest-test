using Microsoft.AspNetCore.Hosting;
using System;

namespace softdesign_test_repository_test
{
    public class BaseTests
    {
        public IServiceProvider ServiceProvider { get; set; }
        private IWebHostBuilder Builder { get; set; }

        public BaseTests()
        {
            Builder = CreateWebHostBuilder();
            ServiceProvider = Builder.Build().Services;
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();

            return builder;
        }
    }
}
