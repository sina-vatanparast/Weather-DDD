using System.Reflection;
using Autofac;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Weatherman.Api.Middlewares;
using Weatherman.Infrastructure;
using Weatherman.Infrastructure.Data;

namespace Weatherman.Api
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        public const string CorsPolicy = "CorsPolicy";
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }

        public void ConfigureDockerServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(c =>
            //    c.UseInMemoryDatabase("AppDb"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            bool isDevelopment = (env.EnvironmentName == "Development");
            builder.RegisterModule(new DefaultInfrastructureModule(isDevelopment, Assembly.GetExecutingAssembly()));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });
             });

            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Azure Service Bus client
            services.AddSingleton<ISenderClient>(_ =>
            {
                var connectionString = Configuration.GetConnectionString("AzureServiceBus");
                return new QueueClient(connectionString, Configuration["AzureServiceBus:QueueName"]);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(CorsPolicy);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weatherman API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI to app root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Default route for controller actions
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
