using Framework.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Seisankanri.Api.Filters;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{

    [BAuthorize]
    [ApiController]
    [Route("api/calendars")]
    public class CalendarController : BaseController
    {
        protected ICalendarService calendarServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public CalendarController(ICalendarService _calendarServices, ILocalizeServices _localServices) : base()
        {
            calendarServices = _calendarServices;
            ls = _localServices;
        }

        /// <summary>
        /// Get list calendar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_company")]
        public async Task<IActionResult> GetByCompany([FromQuery] CalendarSearchRequest calendar)
        {
            var data = await calendarServices.GetByCompany(calendar);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new calendar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CalendarRequest request)
        {

            int count = await calendarServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update calendar by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CalendarRequest request)
        {
            var count = await calendarServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }
    }
}
