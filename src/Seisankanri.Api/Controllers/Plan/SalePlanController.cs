using Business.Core.Extensions;
using Business.Core.Interfaces;
using Business.Plan.Contracts;
using Business.Plan.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Framework.Api.Controllers;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Framework.Core.Helpers.Excel;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [BAuthorize]
    [ApiController]
    [Route("api/rdk_sale_plan")]
    public class SalePlanController : BaseController
    {
        private ISalePlanServices _salePlanServices { get; set; }
        private ILocalizeServices _ls { get; set; }

        public SalePlanController(ISalePlanServices salePlanServices, ILocalizeServices ls)
        {
            _salePlanServices = salePlanServices;
            _ls = ls;
        }

        /// <summary>
        /// Get list get by month
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_month")]
        public async Task<IActionResult> GetAll([FromQuery] SalePlanByMonthRequest request)
        {
            var data = await _salePlanServices.GetByMonth(request);
            if (data != null)
                return Ok(data.ToResponse(_ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        [HttpGet]
        [Route("get_by_branch")]
        public async Task<IActionResult> GetByBranch([FromQuery] SalePlanByBranchRequest request)
        {
            var data = await _salePlanServices.GetByBranch(request);
            if (data != null)
                return Ok(data.ToResponse(_ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// createupdate new saleplan
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Create([FromBody] List<SalePlanRequest> request)
        {
            int count = await _salePlanServices.CreateUpdate(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// export by month
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export_by_branch")]
        public async Task<IActionResult> ExportByBranch([FromQuery] SalePlanByBranchRequest request, string file_name)
        {
            var screen_name = "RDK販売予定入力";
            file_name = file_name ?? $"{screen_name}_{DateTime.Now.ToDateStringFile()}.xlsx";
            var data = await _salePlanServices.GetByBranch(request);
            if (data == null) return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });

            using (var ms = new MemoryStream())
            {
                using (var document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
                {
                    document.CreateWorkbookPart();
                    WorkbookPart workbookpart = document.WorkbookPart;
                    workbookpart.CreateSheet(screen_name);


                    List<ExcelItem> excelItems = new List<ExcelItem>()
                    {
                        new ExcelItem() { header = await _ls.GetAsync(Modules.CORE,  "O_S002", "ClassItem"), key = "item_class_name2" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "ItemCd"), key = "item_cd" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "ItemName1"), key = "item_name1" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "ItemName2"), key = "item_name2" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "OrderUnit"), key = "order_unit" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "StandadUnitQty"), key = "standad_unit_qty" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S002", "OrderYm"), key = "order_ym", type = DataType.MONTH },
                    };
                    var listConvt = ConvertContent.ConvertFlatList<SalePlanByBrandResponse>(data, "data", "company_cd", "order_qty");

                    foreach (var item in data[0].data)
                    {
                        excelItems.Add(new ExcelItem() { header = item.company_name, key = item.company_cd, type = DataType.NUMBER });
                    }
                    excelItems.Add(new ExcelItem() { header = await _ls.GetAsync(Modules.CORE, "O_S002", "Sum"), key = "total", type = DataType.NUMBER });
                    // fill data theo grid
                    workbookpart.FillGridData<dynamic>(listConvt, excelItems);

                    document.Close();
                    return File(ms.ToArray(), "application/octetstream", file_name);
                }
            }

        }

        /// <summary>
        /// export by month
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export_by_month")]
        public async Task<IActionResult> ExportByMonth([FromQuery] SalePlanByMonthRequest request, string file_name)
        {
            var screen_name = "RDK販売予定入力";
            file_name = file_name ?? $"{screen_name}_{DateTime.Now.ToDateStringFile()}.xlsx";
            var data = await _salePlanServices.GetByMonth(request);
            if (data == null) return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });

            using (var ms = new MemoryStream())
            {
                using (var document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
                {
                    document.CreateWorkbookPart();
                    WorkbookPart workbookpart = document.WorkbookPart;
                    workbookpart.CreateSheet(screen_name);

                    List<ExcelItem> excelItems = new List<ExcelItem>()
                    {
                        new ExcelItem() { header = await _ls.GetAsync(Modules.CORE,  "O_S003", "ClassItemTitle"), key = "item_class_name2" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "ItemEdiCd"), key = "item_edi_cd" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "ItemCd"), key = "item_cd" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "ItemName1"), key = "item_name1" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "ItemName2"), key = "item_name2" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "OrderUnit"), key = "order_unit" },
                        new ExcelItem() {  header = await _ls.GetAsync(Modules.CORE, "O_S003", "StandadUnitQty"), key = "standad_unit_qty" },
                    };

                    foreach (var item in data)
                    {
                        foreach (var childItem in item.data)
                        {
                            childItem.order_ym = DateTimeExtensions.GetMonth(DateTime.Parse(childItem.order_ym));
                        }
                    }
                    foreach (var item in data[0].data)
                    {
                        excelItems.Add(new ExcelItem() { header = item.order_ym, key = item.order_ym, type = DataType.NUMBER });
                    }

                    var listConvt = ConvertContent.ConvertFlatList(data, "data", "order_ym", "order_qty");
                    excelItems.Add(new ExcelItem() { header = await _ls.GetAsync(Modules.CORE, "O_S003", "Sum"), key = "total", type = DataType.NUMBER });

                    // fill data theo grid
                    // TODO : fill data by sheet name
                    workbookpart.FillGridData<dynamic>(listConvt, excelItems);

                    document.Close();
                    return File(ms.ToArray(), "application/octetstream", file_name);
                }
            }
        }

        [HttpGet]
        [Route("get_item_plan")]
        public async Task<IActionResult> GetItemPlan([FromQuery] SalePlanByBranchRequest request)
        {
            var data = await _salePlanServices.GetByBranch(request);
            if (data != null)
                return Ok(data.ToResponse(_ls));
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = _ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }
    }
}
