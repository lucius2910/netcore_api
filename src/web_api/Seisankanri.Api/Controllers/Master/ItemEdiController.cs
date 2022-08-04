using Business.Core.Contracts;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Core.Collections;
using Framework.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [BAuthorize]
    [ApiController]
    [Route("api/item_edi")]
    public class ItemEdiController : ControllerBase
    {
        protected IItemEdiServices _itemEdiServices { get; set; }

        protected ILocalizeServices _ls { get; set; }

        public ItemEdiController(IItemEdiServices itemEdiServices, ILocalizeServices ls)
        {
            _itemEdiServices = itemEdiServices;
            _ls = ls;
        }

        /// <summary>
        /// Get by company paged list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get_by_company")]
        public async Task<IActionResult> GetByCompany([FromQuery] ItemEdiPagedRequest request)
        {
            var data = await _itemEdiServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _itemEdiServices.GetById(id);

            if (data != null)
                return Ok(new BaseResponse<ItemEdiResponse>(data, ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_005)));
            else
                return BadRequest(new BaseResponseError(ResponseCode.NotFound, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007)));
        }

        /// <summary>
        /// Create new item_edi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ItemEdiCreateRequest request)
        {
            var count = await _itemEdiServices.Create(request);

            if (count >= 1)
                return Ok(new BaseResponse(ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001)));
            else
                return BadRequest(new BaseResponseError(ResponseCode.SystemError, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001)));
        }

        /// <summary>
        /// Update item_edi by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ItemEdiUpdateRequest request)
        {
            var count = await _itemEdiServices.Update(id, request);

            if (count >= 1)
                return Ok(new BaseResponse(ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002)));
            if (count == -1)
                return BadRequest(new BaseResponse(ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007)));
            else
                return BadRequest(new BaseResponseError(ResponseCode.SystemError, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002)));
        }

        /// <summary>
        /// Delete item_edi by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await _itemEdiServices.Delete(id);

            if (count >= 1)
                return Ok(new BaseResponse(ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003)));
            if (count == -1)
                return BadRequest(new BaseResponse(ResponseCode.Success, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007)));
            else
                return BadRequest(new BaseResponseError(ResponseCode.SystemError, _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003)));
        }
    }
}
