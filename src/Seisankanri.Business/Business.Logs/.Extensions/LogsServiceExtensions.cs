using Business.Logs.Interfaces;
using Business.Logs.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Logs.Extensions
{
    public static class LogsServiceExtensions
    {
        public static IServiceCollection AddLogsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
