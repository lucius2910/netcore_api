using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Framework.Core.Extensions;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Helpers.Cache;
using Framework.Core.Helpers.Email;

namespace Framework.Api.Extensions
{
    public static class InstallerExentions
    {
        public static IServiceCollection AddCoreExtention(this IServiceCollection services)
        {
            // FluentValidation
            services.AddFluentValidation(c =>
            {
                c.DisableDataAnnotationsValidation = true;
                c.ImplicitlyValidateChildProperties = true;
                c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });


            services.AddScoped<IServices, BaseServices>();

            services.AddMemoryCache();
            services.AddTransient<ICachingService, DefaultCachingService>();
            services.AddTransient<IEmailHelper, EmailHelper>();

            return services;
        }
    }
}
