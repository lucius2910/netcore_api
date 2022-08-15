using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Sale.Interfaces;
using Application.Sale.Services;

namespace Application.Sale.Extensions
{
    public static class AddApplicationSaleService
    {
        public static IServiceCollection AddSaleService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISalePlanServices, SalePlanServices>();
            return services;
        }
    }
}
