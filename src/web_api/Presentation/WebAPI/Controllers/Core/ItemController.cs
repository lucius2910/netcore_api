using Microsoft.AspNetCore.Mvc;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Application.Common.Abstractions;
using Application.Common.Extensions;
using Framework.Core.Collections;
using WebAPI.Filters;

namespace WebAPI.Controllers.Core
{
    /// <summary>
    /// 
    /// </summary>
    [BAuthorize]
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private IItemServices _itemServices { get; set; }
        private ILocalizeServices _ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemServices"></param>
        /// <param name="ls"></param>
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
        public async Task<IActionResult> GetPagedList([FromQuery] PagedRequest request)
        {
            var data = await _itemServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get_list_by_type")]
        public async Task<IActionResult> GetList([FromQuery] ItemTypePagedRequest request)
        {
            var data = await _itemServices.GetListByType(request);
            return Ok(data);
        }
    }
}
