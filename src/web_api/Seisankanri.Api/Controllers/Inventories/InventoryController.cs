using Microsoft.AspNetCore.Mvc;
using Business.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Framework.Api.Controllers;
using Business.Inventories.Interfaces;
using Business.Inventories.Contracts;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : BaseController
    {
        protected IInventoryServices inventoryServices { get; set; }
        protected ILocalizeServices ls { get; set; }
        public InventoryController(IInventoryServices _inventoryServices, ILocalizeServices ls) : base()
        {
            inventoryServices = _inventoryServices;
            this.ls = ls;
        }

        /// <summary>
        /// Get list inventory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] InventoryPagedRequest request)
        {
            var data = await inventoryServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get inventory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await inventoryServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new inventory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventoryRequest request)
        {
            int count = await inventoryServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.RESOURCE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update inventory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InventoryRequest request)
        {
            var count = await inventoryServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete inventory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await inventoryServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        /// <summary>
        /// Delete inventory by ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-multi")]
        public async Task<IActionResult> Delete([FromBody] Guid[] ids)
        {
            var count = await inventoryServices.Delete(ids);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }
    }
}
