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
    [Route("api/machine")]
    public class MachineController : ControllerBase
    {
        private IMachineServices machineServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_machineServices"></param>
        /// <param name="_ls"></param>
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
        public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
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
                return Ok(data.ToResponse());
            else
                return BadRequest(data.ToResponse());
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Machine", "CreateSuccess") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Common", "HasError") });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Machine", "UpdateSuccess") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Machine", "UpdateError") });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Machine", "DeleteSuccess") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Common", "HasError") });
        }

        /// <summary>
        /// Delete machine by ids
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-multi")]
        public async Task<IActionResult> Delete([FromBody] Guid[] ids)
        {
            var count = await machineServices.Delete(ids);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Machine", "DeleteSuccess") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Common", "HasError") });
        }
    }
}
