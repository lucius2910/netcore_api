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
    [Route("api/receive_order_dt")]
    public class ReceiveOrderDtController : ControllerBase
    {
        private IReceiveOrderDtServices roDtServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_roDtServices"></param>
        /// <param name="_ls"></param>
        public ReceiveOrderDtController(IReceiveOrderDtServices _roDtServices, ILocalizeServices _ls) : base()
        {
            roDtServices = _roDtServices;
            ls = _ls;
        }


        /// <summary>
        /// Get ReceiveOrderDt by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await roDtServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse());
            else
                return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_007) });
        }

        /// <summary>
        /// Update ReceiveOrderDt by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] ReceiveOrderDtRequest request)
        {
            var count = await roDtServices.UpdateById(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_002) });
            else
                return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_002) });
        }

        /// <summary>
        /// Update ReceiveOrderDt by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var count = await roDtServices.DeleteById(id);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_003) });
            else if (count == -1)
                return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_005) });
            else
                return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_003) });
        }
    }
}
