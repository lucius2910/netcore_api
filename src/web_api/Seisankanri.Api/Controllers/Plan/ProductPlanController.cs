using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Api.Controllers;
using Framework.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Filters;
using Business.Plan.Contracts;
using Business.Plan.Interfaces;
using Framework.Core.Extensions;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    [ApiController]
    [BAuthorize]
    [Route("api/rdk_product_plan")]
    public class ProductPlanController : BaseController
    {
        private IProductPlanService productPlanService { get; set; }
        private ILocalizeServices ls { get; set; }

        public ProductPlanController(IProductPlanService _productPlanDayService, ILocalizeServices _ls) : base()
        {
            productPlanService = _productPlanDayService;
            ls = _ls;
        }

        ///
        [HttpGet]
        [Route("get_by_month")]
        public async Task<IActionResult> GetByMonth([FromQuery] ProductPlanMonthRequest request)
        {
            var data = await productPlanService.GetByMonth(request);
            if (data != null)   
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Update product plan day
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_by_day")]
        public async Task<IActionResult> UpdateByMonth([FromBody] List<ProductPlanUpdateRequest> requests)
        {
            var count = await productPlanService.UpdateByMonth(requests);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// delete by item product plan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete_by_item")]
        public async Task<IActionResult> DeleteByItem(ProductPlanDeleteByItemRequest request)
        {
            var count = await productPlanService.DeleteByItem(request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else if (count == -1)
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("get_by_day")]
        public async Task<IActionResult> GetByDay([FromQuery] ProductPlanDaySearchRequest request)
        {
            var data = await productPlanService.GetByDay(request);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// init by item product plan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("init_data_by_month")]
        public async Task<IActionResult> InitDataByMonth([FromQuery] InitDataByMonthRequest request)
        {
            var data = await productPlanService.InitDataByMonth(request);
            if(data == 0)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.W_012) });

            var productPlanDay = await productPlanService.GetByDay(new ProductPlanDaySearchRequest() {plan_month = request.order_ym});

            if (productPlanDay != null)
                return Ok(productPlanDay.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }
    }
}

