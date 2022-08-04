using Framework.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Extensions
{
    public static class FrameworkExtensions
    {
        public static IServiceCollection InstallApplication<T>(this IServiceCollection services, IConfiguration configuration) where T : IApplicationInstaller
        {
            var installer = Activator.CreateInstance<T>();
            installer.Install(services, configuration);
            return services;
        }

    }
}
