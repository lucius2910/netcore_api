using Microsoft.AspNetCore.Mvc;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using WebAPI.Filters;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Application.Common.Abstractions;
using Application.Common.Extensions;

namespace WebAPI.Controllers.Core
{
    /// <summary>
    /// 
    /// </summary>
    [BAuthorize]
    [ApiController]
    [Route("api/calendars")]
    public class CalendarController : ControllerBase
    {
        private ICalendarService calendarServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_calendarServices"></param>
        /// <param name="_localServices"></param>
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
        public async Task<IActionResult> GetByCompany([FromQuery] CalendarSearchRequest request)
        {
            var data = await calendarServices.GetByCompany(request);
            if (data != null)
                return Ok(data.ToResponse());
            else
                return BadRequest(data.ToResponse());
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Message", "I_001") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Message", "E_001") });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, "Message", "I_002") });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, "Message", "E_002") });
        }
    }
}
