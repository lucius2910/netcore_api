using Business.Core.Contracts;
using Business.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [Route("api/system_settings")]
    [ApiController]
    [BAuthorize]


    public class SystemSettingController : ControllerBase
    {
        protected ISystemSettingsServices systemsettingsServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public SystemSettingController(ISystemSettingsServices _systemsettingsServices,
            ILocalizeServices _ls)
        {
            systemsettingsServices = _systemsettingsServices;
            ls = _ls;
        }


        /// <summary>
        /// Get list system_settings
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetAll([FromQuery] SystemSettingsPagedRequest request)
        {
            var data = await systemsettingsServices.GetPaged(request);
            return Ok(data);
        }


    }
}
