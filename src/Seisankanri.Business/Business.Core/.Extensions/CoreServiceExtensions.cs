using Business.Core.Services;
using Business.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Business.Core.Extensions
{
    public static class CoreServiceExtensions
    {
        public static IServiceCollection AddCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IFunctionServices, FunctionServices>();
            services.AddScoped<IResourceServices, ResourceServices>();
            services.AddScoped<IMasterCodeServices, MasterCodeServices>();
            services.AddScoped<IResourceServices, ResourceServices>();
            services.AddScoped<IMachineServices, MachineServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<IItemEdiServices, ItemEdiServices>();
            services.AddScoped<ICompanyServices, CompanyServices>();
            services.AddScoped<IItemPriceMasterService, ItemPriceMasterService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IClassificationServices, ClassificationServices>();
            services.AddScoped<ISeqServices, SeqServices>();
            services.AddScoped<ISystemSettingsServices, SystemSettingsServices>();
            
            return services;
        }
    }
}
