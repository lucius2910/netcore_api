using Business.Plan.Interfaces;
using Business.Plan.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Plan.Extensions
{
    public static class PlanServiceExtensions
    {
        public static IServiceCollection AddPlanService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISalePlanServices, SalePlanServices>();
            services.AddScoped<IProductPlanService, ProductPlanService>();
            return services;
        }
    }
}
