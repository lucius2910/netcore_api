using Business.Core.Extensions;
using Business.Core.Interfaces;
using Business.SaleOrder.Contracts;
using Business.SaleOrder.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
    [Route("api/receive_order")]
    public class ReceiveOrdersController : BaseController
    {
        protected IReceiveOrderServices receiveOrderService { get; set; }
        protected ILocalizeServices ls { get; set; }

        public ReceiveOrdersController(IReceiveOrderServices _receiveOrderService, ILocalizeServices _ls) : base()
        {
            receiveOrderService = _receiveOrderService;
            ls = _ls;
        }

        /// <summary>
        /// createupdate new receive orders
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ReceiveOrderRequest request)
        {
            int count = await receiveOrderService.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                if(count == -1)
                    return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
                else
                    return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }
        /// <summary>
        /// Get list receiveOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetListPaged([FromQuery] ReceiveOrderSearchRequest request)
        {
            var data = await receiveOrderService.GetPaged(request);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        [HttpGet]
        [Route("create_output_file")]
        public async Task<IActionResult> GetListData([FromQuery] Guid id)
        {
            string fileName = "receive_order_" + DateTime.Now.Year + DateTime.Now.Millisecond + ".xlsx";
            var data = await receiveOrderService.GetData(id);
            if (data == null)
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });


            // tạo file excel
            using (var ms = new MemoryStream())
            {
                using (var document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
                {
                    document.CreateWorkbookPart();
                    WorkbookPart workbookpart = document.WorkbookPart;
                    workbookpart.CreateSheet("data");
                    // khai báo cell
                    Dictionary<string, string> generalInformation = new Dictionary<string, string>();
                    generalInformation.Add("D5", data.order_company_name1);
                    generalInformation.Add("D6", data.branch_f.ToString());
                    generalInformation.Add("D7", data.order_post_code);
                    generalInformation.Add("D8", data.order_address1);
                    generalInformation.Add("D9", data.order_tel_no);
                    generalInformation.Add("E9", data.order_fax_no);
                    generalInformation.Add("J5", data.delivery_company_name1);
                    generalInformation.Add("L5", data.customer_person_name);
                    generalInformation.Add("J6", data.delivey_post_code);
                    generalInformation.Add("L6", data.delivery_address1);
                    generalInformation.Add("L7", data.address2);
                    generalInformation.Add("K9", data.delivery_tel_no);
                    generalInformation.Add("M9", data.delivery_fax_no);
                    generalInformation.Add("O5", data.r_order_date);
                    
                     
                    // BorderAll
                    // BorderTop
                    // BorderRight
                    // BorderLeft
                    // BorderBottom

                    // fill data theo cell
                    workbookpart.FillCellInfomation(generalInformation);

                    // khai báo grid
                    List<ExcelItem> excelItems = new List<ExcelItem>()
                    {
                        new ExcelItem() { header = await ls.GetAsync(Modules.CORE,  "ReceiveOrderDt", "r_order_id"), key = "id", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "item_cd"), key = "item_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "item_name"), key = "item_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "r_order_input"), key = "r_order_input", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "r_order_qty"), key = "r_order_qty", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "r_order_unit_price"), key = "r_order_unit_price", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "r_order_cost"), key = "r_order_cost", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "r_order_dl"), key = "r_order_dl", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                        new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "ReceiveOrderDt", "remarks"), key = "remarks", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
                    };

                    // fill data theo grid
                    workbookpart.FillGridData<ReceiveOrderDtResoponse>(data.details, excelItems);
                    workbookpart.IsBold("D5");
                    workbookpart.FillColor("J6", "CDC1C1");
                    workbookpart.SetBorder("J6", true, true, true, true, BorderStyleValues.Dotted);
                    document.Close();
                    return File(ms.ToArray(), "application/octetstream", fileName);
                }
            }

        }
    }
}
