using Framework.Api.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Helpers.Auth;
using Microsoft.Extensions.Options;

namespace Seisankanri.Api.Extensions
{
    public class ApiServiceContext : IServiceContext
    {
        public Guid? _userId { get; set; }
        public IOptions<AuthSetting> _settings { get; set; }
        public IHttpContextAccessor _accessor { get; set; }

        public ApiServiceContext(IOptions<AuthSetting> settings, IHttpContextAccessor accessor)
        {
            _settings = settings;
            _accessor = accessor;
            var bearerToken = accessor.HttpContext.Request.Headers["Authorization"];
            var token = !string.IsNullOrEmpty(bearerToken) ? bearerToken.ToString().Substring("Bearer ".Length) : null;
            _userId = JwtHelpers.ValidateToken(token, settings.Value.JWTSecret);
        }

    }
}
