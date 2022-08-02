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
    [ApiController]
    [BAuthorize]
    [Route("api/master_code")]
    public class MasterCodeController : BaseController
    {
        protected IMasterCodeServices masterCodeServices { get; set; }
        protected ILocalizeServices ls { get; set; }

        public MasterCodeController(IMasterCodeServices _masterCodeServices,ILocalizeServices _localServices) : base()
        {
            masterCodeServices = _masterCodeServices;
            ls = _localServices;
        }

        /// <summary>
        /// Get list master code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MasterCodePagedRequest request)
        {
            var data = await masterCodeServices.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get list master code by type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_type")]
        public async Task<IActionResult> GetByTypePaged([FromQuery] MasterCodePagedByTypeRequest request)
        {
            var data = await masterCodeServices.GetByTypePaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get masterCode by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await masterCodeServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// Create new masterCode
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MasterCodeRequest request)
        {
            int count = await masterCodeServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Update masterCode by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MasterCodeRequest request)
        {
            var count = await masterCodeServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete masterCode by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await masterCodeServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        /// <summary>
        /// Delete masterCode by ids
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-multi")]
        public async Task<IActionResult> DeleteMulti([FromBody] Guid[] ids)
        {
            var count = await masterCodeServices.Delete(ids);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003 ) });
        }

        [HttpGet]
        [Route("export_csv")]
        public async Task<FileResult> ExportCsv([FromQuery] MasterCodePagedRequest request, [FromQuery] string file_name)
        {

            var dataResponse = await masterCodeServices.GetPaged(request);

            List<CsvItem> csvItems = new List<CsvItem>()
            {   
                new CsvItem() { header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.TYPE), key = "type", type = DataType.TEXT },
                new CsvItem() {  header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.KEY), key = "key", type = DataType.TEXT },
                new CsvItem() {  header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.VALUE), key = "value", type = DataType.TEXT }
            };

            var fileStream = await CsvHelpers.ExportCsv(dataResponse.data.ToList(), csvItems);
            return File(fileStream, "application/octetstream", file_name);
        }

        [HttpGet]
        [Route("export_excel")]
        public async Task<IActionResult> ExportExel([FromQuery] MasterCodePagedRequest request, [FromQuery] string file_name)
        {
            var dataResponse = await masterCodeServices.GetPaged(request);

            List<ExcelItem> excelItems = new List<ExcelItem>()
            {
                new ExcelItem() { header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.TYPE), key = "type", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.KEY), key = "key", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, Screen.MASTERCODE, MessageKey.VALUE), key = "value", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "MasterCode", "asd"), key = "created_at", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }
    }
}
