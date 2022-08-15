using Microsoft.AspNetCore.Mvc;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Application.Common.Abstractions;
using Application.Common.Extensions;
using WebAPI.Filters;
using Framework.Core.Helpers.Excel;

namespace WebAPI.Controllers.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/classification")]
    [ApiController]
    [BAuthorize]
    public class ClassificationController : ControllerBase
    {
        private IClassificationServices classificationServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_classificationServices"></param>
        /// <param name="_localServices"></param>
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
        public async Task<IActionResult> GetList([FromQuery] ClassificationSearchRequest request)
        {
            var data = await classificationServices.GetList(request);
            if (data != null)
                return Ok(data.ToResponse());
            else
                return BadRequest(data.ToResponse());
        }

        /// <summary>
        /// Create new classification
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Create([FromBody] List<ClassificationRequest>  requests)
        {
            int count =  await classificationServices.CreateUpdate(requests);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_001) });
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
                new ExcelItem() { header = await ls.GetAsync(Modules.Core, "Classification", "Code1"), key = "code1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Classification", "Code2"), key = "code2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Classification", "Name1"), key = "name1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Classification", "Name2"), key = "name2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }
    }
}
