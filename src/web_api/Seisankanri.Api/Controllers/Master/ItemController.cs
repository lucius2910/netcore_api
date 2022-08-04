using Business.Core.Contracts;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Core.Collections;
using Framework.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [BAuthorize]
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        protected IItemServices _itemServices { get; set; }
        protected ILocalizeServices _ls { get; set; }

        public ItemController(IItemServices itemServices, ILocalizeServices ls)
        {
            _itemServices = itemServices;
            _ls = ls;
        }

        /// <summary>
        /// Get paged list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("get_list")]
        public async Task<IActionResult> GetPagedList([FromQuery] ItemSearchRequest request)
        {
            var data = await _itemServices.GetPaged(request);
            return Ok(data);
        }

        [HttpGet("get_list_by_type")]
        public async Task<IActionResult> GetList([FromQuery] ItemTypePagedRequest request)
        {
            var data = await _itemServices.GetListByType(request);
            if (data != null)
                return Ok(data.ToResponse(_ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _itemServices.GetById(id);
            if (data != null)
            {
                return Ok(new BaseResponse<ItemResponse>(data, ResponseCode.Success, null));
            }
            else
            {
                return BadRequest(new BaseResponseError(ResponseCode.NotFound, null));
            }
        }
    }
}
