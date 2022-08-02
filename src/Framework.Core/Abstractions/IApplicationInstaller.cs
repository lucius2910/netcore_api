using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Abstractions
{
    public interface IApplicationInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
        Task InstallAsync(IServiceCollection services, IConfiguration configuration);
    }
}
