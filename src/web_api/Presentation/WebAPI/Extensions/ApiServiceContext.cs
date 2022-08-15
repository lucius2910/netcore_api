using Application.Common.Abstractions;
using Framework.Core.Helpers.Auth;
using Infrastructure.Contracts;
using Microsoft.Extensions.Options;

namespace WebAPI.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiServiceContext : ICurrentUserService
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? user_id { get; set; }
        
        private IOptions<AuthSetting> _settings { get; set; }
        private IHttpContextAccessor _accessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="accessor"></param>
        public ApiServiceContext(IOptions<AuthSetting> settings, IHttpContextAccessor accessor)
        {
            _settings = settings;
            _accessor = accessor;
            var bearerToken = accessor.HttpContext.Request.Headers["Authorization"];
            var token = !string.IsNullOrEmpty(bearerToken) ? bearerToken.ToString().Substring("Bearer ".Length) : null;
            user_id = JwtHelpers.ValidateToken(token, settings.Value.JWTSecret);
        }

    }
}
