using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Inventories.Interfaces;
using Application.Inventories.Services;

namespace Application.Inventories.Extensions
{
    public static class AddApplicationInventoryServices
    {
        public static IServiceCollection AddInventoryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWarehouseServices, WarehouseService>();
            return services;
        }
    }
}
