using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using softdesign_test_dependency_injection;
using System;
using System.IO;
using System.Reflection;

namespace softdesign_test_api
{
    public class Startup
    {
        private readonly string ApplicationName;
        private readonly string ApplicationVersion;

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ApplicationName = "SoftDesignTest";
            ApplicationVersion = "v1";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var mongoDbDatabase = Configuration.GetValue<string>("MongoConnection:Database");
            var mongoDbConnectionString = Configuration.GetValue<string>("MongoConnection:ConnectionString");

            Dependency.RegisterAll(services, mongoDbDatabase, mongoDbConnectionString);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApplicationVersion, new OpenApiInfo
                {
                    Title = ApplicationName,
                    Version = ApplicationVersion,
                    Description = ""
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationName);
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
