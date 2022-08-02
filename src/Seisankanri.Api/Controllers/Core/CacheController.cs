using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Api.Controllers;
using Framework.Core.Helpers;
using Framework.Core.Helpers.Cache;
using Microsoft.AspNetCore.Mvc;

namespace Seisankanri.Api.Controllers
{
    [ApiController]
    [Route("api/cache")]
    public class CacheController : BaseController
    {
        private readonly ICachingService cachingService;
        private readonly ILocalizeServices ls;

        public CacheController(ICachingService _cachingService, ILocalizeServices _ls)
        {
            cachingService = _cachingService;
            ls = _ls;

        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(null);
        }

        [HttpDelete]
        [Route("clear-all")]
        public async Task<IActionResult> ClearAll()
        {
            await cachingService.ClearAllAsync();
            return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.CACHE, MessageKey.CLEARALLSUCCESS) });
        }

        [HttpDelete]
        [Route("clear-key")]
        public async Task<IActionResult> ClearByKey([FromBody] string[] keys)
        {
            await cachingService.ClearAsync(keys);
            return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.CACHE, MessageKey.CLEARALLSUCCESS) });
        }
    }
}
