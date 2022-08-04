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
    [BAuthorize]
    [ApiController]
    [Route("api/function")]
    public class FunctionController : BaseController
    {
        protected IFunctionServices functionServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public FunctionController(IFunctionServices _functionServices, ILocalizeServices _ls) : base()
        {
            functionServices = _functionServices;
            ls = _ls;
        }

        /// <summary>
        /// Get list Function
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")] 
        public async Task<IActionResult> GetPaged([FromQuery] FunctionPagedRequest request)
        {
            var data = await functionServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get Function by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await functionServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new Function
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] FunctionRequest request)
        {
            int count = await functionServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001 ) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update Function by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FunctionRequest request)
        {
            var count = await functionServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete Function by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await functionServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }


        [HttpGet]
        [Route("menu")]
        public async Task<IActionResult> GetMenu()
        {
            var data = await functionServices.GetAsTreeView().ToResponse(ls);
            return Ok(data);
        }
    }
}

