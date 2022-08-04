using Business.Core.Extensions;
using Business.Core.Interfaces;
using Business.SaleOrder.Contracts;
using Business.SaleOrder.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [ApiController]
    [BAuthorize]
    [Route("api/receive_order_dt")]
    public class ReceiveOrderDtController : ControllerBase
    {
        protected IReceiveOrderDtServices roDtServices { get; set; }
        protected ILocalizeServices ls { get; set; }

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
                return Ok(data.ToResponse(ls));
            else
                return Ok(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Update ReceiveOrderDt by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var count = await roDtServices.DeleteById(id);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else if (count == -1)
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }
    }
}
