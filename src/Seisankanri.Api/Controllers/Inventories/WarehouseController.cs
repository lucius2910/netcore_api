using Microsoft.AspNetCore.Mvc;
using Business.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Framework.Api.Controllers;
using Business.Inventories.Interfaces;
using Business.Inventories.Contracts;
using Seisankanri.Model.Entities;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : BaseController
    {
        protected IWarehouseServices warehouseServices { get; set; }
        protected ILocalizeServices ls { get; set; }
        public WarehouseController(IWarehouseServices _warehouseServices, ILocalizeServices ls) : base()
        {
            warehouseServices = _warehouseServices;
            this.ls = ls;
        }

        /// <summary>
        /// Get list warehouse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetAll([FromQuery] WarehousePagedRequest request)
        {
            var data = await warehouseServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get warehouse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await warehouseServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new warehouse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] WarehouseRequest request)
        {
            int count = await warehouseServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update warehouse by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WarehouseRequest request)
        {
            var count = await warehouseServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete warehouse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await warehouseServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        //todo: export excel
    }
}
