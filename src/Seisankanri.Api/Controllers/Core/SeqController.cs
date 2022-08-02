using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Api.Controllers;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{

    [ApiController]
    [BAuthorize]
    [Route("api/code")]
    public class SeqController : BaseController
    {
        private ISeqServices seqServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_seqServices"></param>
        /// <param name="_localServices"></param>
        public SeqController(ISeqServices _seqServices, ILocalizeServices _localServices) : base()
        {
            seqServices = _seqServices;
            ls = _localServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{type}")]
        public async Task<IActionResult> GetByType([FromRoute]string type)
        {
            var data = await seqServices.ApiGenSeqType(type);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }



    }
}
