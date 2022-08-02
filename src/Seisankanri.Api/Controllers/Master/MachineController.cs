using Framework.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Seisankanri.Api.Filters;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    
    [ApiController]
    [BAuthorize]
    [Route("api/machine")]
    public class MachineController : BaseController
    {
        protected IMachineServices machineServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public MachineController(IMachineServices _machineServices, ILocalizeServices _ls) : base()
        {
            machineServices = _machineServices;
            ls = _ls;
        }


        /// <summary>
        /// Get list machine
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MachinePagedRequest request)
        {
            var data = await machineServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get list machine
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetPaged([FromQuery] MachinePagedRequest request)
        {
            var data = await machineServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get machine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await machineServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// tax new machine
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MachineRequest request)
        {
            int count = await machineServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update machine by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MachineRequest request)
        {
            var count = await machineServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete machine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await machineServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        /// <summary>
        /// Delete machine by ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-multi")]
        public async Task<IActionResult> Delete([FromBody] Guid[] ids)
        {
            var count = await machineServices.Delete(ids);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }
    }
}
