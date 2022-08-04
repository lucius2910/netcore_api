using Framework.Api.Controllers;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Seisankanri.Api.Filters;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        protected IAuthServices authServices { get; set; }
        protected IUserServices userServices { get; set; }
        protected IServiceContext servicesContext { get; set; }
        protected ILocalizeServices ls { get; set; }

        public AuthController(IAuthServices _authServices,
            IUserServices _userServices,
            ILocalizeServices _ls,
            IServiceContext _servicesContext) 
        {
            authServices = _authServices;
            userServices = _userServices;
            ls = _ls;
            servicesContext = _servicesContext;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
        {
            var res = await authServices.Login(request);
            if(res != null)
                return Ok(res.ToResponse(ls));
            else
                return Unauthorized(res.ToResponse(ls));
            
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BAuthorize]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refresh_token)
        {
            var res = await authServices.Refresh(refresh_token);
            if (res != null)
                return Ok(res.ToResponse(ls));
            else
                return Unauthorized(res.ToResponse(ls));
        }

        /// <summary>
        /// Get info user login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BAuthorize]
        [Route("info")]
        public async Task<IActionResult> GetInfoLogin()
        {
            var data = await userServices.GetInfoLoginById((Guid)servicesContext._userId);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [BAuthorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]AuthChangePassRequest request)
        {
            var count = await authServices.ChangePassword((Guid)servicesContext._userId, request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }
    }
}
