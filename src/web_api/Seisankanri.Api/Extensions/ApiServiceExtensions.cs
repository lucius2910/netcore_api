using Business.Core.Interfaces;
using Business.Core.Services;
using FluentValidation.AspNetCore;
using Framework.Api.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Seisankanri.Api.ProfileMapper;
using Seisankanri.Model;
using System.Reflection;

namespace Seisankanri.Api.Extensions
{
    public static class APIServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Databse
            var connectionString = configuration.GetConnectionString("CoreDb");

            // Core
            var migrationsAssemblyCore = typeof(CoreDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<CoreDbContext>(builder =>
            {
                builder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssemblyCore));
                builder.LogTo(Console.WriteLine);
            });

            // Register unit of work
            services.AddUnitOfWork<CoreDbContext>();

            return services;
        }

        public static IServiceCollection AddCORS(this IServiceCollection services, string name)
        {
            // CORS
            services.AddCors(builder =>
            {
                builder.AddPolicy(
                    name: name,
                    policy =>
                        {
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                            policy.AllowAnyOrigin();
                        }
                    );
            });

            return services;
        }

        public static IServiceCollection AddServiceContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSetting>(configuration.GetSection("AuthSetting"));
            services.AddScoped<IServiceContext, ApiServiceContext>();

            // Adding the AutoMapper to DI container
            services.AddAutoMapper(typeof(CoreProfile));

            services.AddTransient<ILocalizeServices, LocalizeServices>();

            // FluentValidation intercept
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();

            return services;
        }


    }
}
