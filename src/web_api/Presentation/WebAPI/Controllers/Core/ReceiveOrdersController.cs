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
    [ApiController]
    [BAuthorize]
    [Route("api/receive_order")]
    public class ReceiveOrdersController : ControllerBase
    {
        private IReceiveOrderServices receiveOrderService { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_receiveOrderService"></param>
        /// <param name="_ls"></param>
        public ReceiveOrdersController(IReceiveOrderServices _receiveOrderService, ILocalizeServices _ls) : base()
        {
            receiveOrderService = _receiveOrderService;
            ls = _ls;
        }

        /// <summary>
        /// createupdate new receive orders
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ReceiveOrderRequest request)
        {
            int count = await receiveOrderService.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_002) });
            else
                if(count == -1)
                    return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, "S_S001_E_001") });
                else
                    return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_002) });
        }
        /// <summary>
        /// Get list receiveOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetListPaged([FromQuery] ReceiveOrderSearchRequest request)
        {
            var data = await receiveOrderService.GetPaged(request);
            if (data != null)
                return Ok(data.ToResponse());
            else
                return Ok(data.ToResponse());
        }
    }
}
