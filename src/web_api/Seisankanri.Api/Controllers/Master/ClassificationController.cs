using Business.Core.Contracts;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Api.Controllers;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Framework.Core.Helpers.Excel;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [Route("api/classification")]
    [ApiController]
    [BAuthorize]
    public class ClassificationController : BaseController
    {
        protected IClassificationServices classificationServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public ClassificationController(IClassificationServices _classificationServices, ILocalizeServices _localServices) : base()
        {
            classificationServices = _classificationServices;
            ls = _localServices;
        }

        /// <summary>
        /// Get list classification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList([FromQuery] ClassificationSearchRequest classification)
        {
            var data = await classificationServices.GetList(classification);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new classification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Create([FromBody] List<ClassificationRequest>  requests)
        {
            int count =  await classificationServices.CreateUpdate(requests);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="file_name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export_excel")]
        public async Task<IActionResult> ExportExel([FromQuery] ClassificationSearchRequest request, [FromQuery] string file_name)
        {
            var dataResponse = await classificationServices.GetList(request);

            List<ExcelItem> excelItems = new List<ExcelItem>()
            {
                new ExcelItem() { header = await ls.GetAsync(Modules.CORE, "Classification", "Code1"), key = "code1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Classification", "Code2"), key = "code2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Classification", "Name1"), key = "name1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Classification", "Name2"), key = "name2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }

        /// <summary>
        /// get by type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_type/{type}")]
        public async Task<IActionResult> GetByType([FromRoute] ClassificationGetByTypeRequest request)
        {
            var data = await classificationServices.GetByType(request);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }
    }
}
