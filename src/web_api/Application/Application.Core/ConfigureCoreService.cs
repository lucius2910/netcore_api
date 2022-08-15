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
            services.AddTransient<ILocalizeServices, LocalizeServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IFunctionServices, FunctionServices>();
            services.AddScoped<IMasterCodeServices, MasterCodeServices>();
            services.AddScoped<IMachineServices, MachineServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<ICompanyServices, CompanyServices>();
            services.AddScoped<IItemPriceMasterService, ItemPriceMasterService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IClassificationServices, ClassificationServices>();
            services.AddScoped<IReceiveOrderDtServices, ReceiveOrderDtServices>();
            services.AddScoped<IReceiveOrderServices, ReceiveOrderServices>();

            services.AddScoped<IResourceServices, ResourceServices>();
            services.AddScoped<ILogServices, LogServices>();
            return services;
        }
    }
}
