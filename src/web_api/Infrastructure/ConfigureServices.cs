using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Databse
            var connectionString = configuration.GetConnectionString("CoreDb");

            // Core
            var migrationsAssemblyCore = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssemblyCore));
                builder.LogTo(Console.WriteLine);
            });

            // Register unit of work
            services.AddUnitOfWork<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

    }
}
