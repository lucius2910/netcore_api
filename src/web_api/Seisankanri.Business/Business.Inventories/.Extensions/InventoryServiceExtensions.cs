using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Business.Inventories.Interfaces;
using Business.Inventories.Services;

namespace Business.Inventories.Extensions
{
    public static class InventoryServiceExtensions
    {
        public static IServiceCollection AddInventoryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IInventoryServices, InventoryServices>();
            services.AddScoped<IWarehouseServices, WarehouseService>();
            return services;
        }
    }
}
