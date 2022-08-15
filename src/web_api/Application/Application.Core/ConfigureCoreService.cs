using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Core.Services;
using Application.Core.Interfaces;
using Application.Common.Abstractions;

namespace Application.Core.Extensions
{
    public static class ConfigureCoreService
    {
        public static IServiceCollection AddCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILocalizeServices, LocalizeServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IFunctionServices, FunctionServices>();
            services.AddScoped<IMasterCodeServices, MasterCodeServices>();

            services.AddScoped<IResourceServices, ResourceServices>();
            services.AddScoped<ILogServices, LogServices>();
            return services;
        }
    }
}
