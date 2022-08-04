using Business.SaleOrder.Interfaces;
using Business.SaleOrder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.SaleOrder.Extensions
{
    public static class SaleOrderServiceExtensions
    {
        public static IServiceCollection AddSaleOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IReceiveOrderDtServices, ReceiveOrderDtServices>();
            services.AddScoped<IReceiveOrderServices, ReceiveOrderServices>();

            return services;
        }
    }
}
