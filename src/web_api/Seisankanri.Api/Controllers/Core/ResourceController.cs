using Framework.Api.Controllers;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Framework.Core.Abstractions;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{

    [ApiController]
    [Route("api/resource")]
    public class ResourceController : BaseController
    {
        protected IResourceServices resourceServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public ResourceController(IResourceServices _resourceServices, ILocalizeServices _localServices) : base()
        {
            resourceServices = _resourceServices;
            ls = _localServices;
        }

        /// <summary>
        /// Get list Resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ResourcePagedRequest request)
        {
            var data = await resourceServices.GetPaged(request);
            return Ok(data);
        }
        /// <summary>
        /// Get Resource by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await resourceServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new Resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResourceRequest request)
        {
            int count = await resourceServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update Resource by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ResourceRequest request)
        {
            var count = await resourceServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete Resource by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await resourceServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        /// <summary>
        /// Get list Screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("screens")]
        public async Task<IPagedList<string>?> GetListScreen([FromQuery] ResourcePagedRequest request)
        {
            return await resourceServices.GetScreensByModule(request);
        }
    }
}
