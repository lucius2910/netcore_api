using Framework.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Seisankanri.Api.Filters;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Business.Core.Extensions;
using Framework.Core.Helpers.Excel;
using Seisankanri.Api.Extensions;

namespace Seisankanri.Api.Controllers
{
    [BAuthorize]
    [ApiController]
    [Route("api/item_price")]

    public class ItemPriceMastersController : BaseController
    {
        protected IItemPriceMasterService itemPriceMasterService { get; set; }
        protected ILocalizeServices ls { get; set; }

        public ItemPriceMastersController(IItemPriceMasterService _itemPriceMasterService, ILocalizeServices _ls) : base()
        {
            itemPriceMasterService = _itemPriceMasterService;
            ls = _ls;
        }

        /// <summary>
        /// Get list ItemPriceMaster
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetAll([FromQuery] ItemPricePagedRequest request)
        {
            var data = await itemPriceMasterService.GetPaged(request);
            return Ok(data);
        }

        /// <summary>
        /// Get ItemPriceMaster by id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_code/{code}")]
        public async Task<IActionResult> GetByCode(String code)
        {
            var data = await itemPriceMasterService.GetByCode(code);
            if (data != null)
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
        }

        /// <summary>
        /// createupdate new ItemPriceMaster
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create_update")]
        public async Task<IActionResult> Create([FromBody] ItemPriceMasterRequest request)
        {
            int count = await itemPriceMasterService.CreateUpdate(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
        }

        /// <summary>
        /// Delete sale by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete_sale_by_id/{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var count = await itemPriceMasterService.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        [HttpGet]
        [Route("export_excel")]
        public async Task<IActionResult> ExportExel([FromQuery] ItemPricePagedRequest request, [FromQuery] string file_name)
        {
            var dataResponse = await itemPriceMasterService.GetPaged(request);

            List<ExcelItem> excelItems = new List<ExcelItem>()
            {
                new ExcelItem() { header = ls.Get(Modules.CORE, "Item", "ItemType"), key = "item_type", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "Item", "ItemCode"), key = "item_code", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "Item", "ItemName1"), key = "item_name1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "Item", "ItemName2"), key = "item_name2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() { header = ls.Get(Modules.CORE, "ItemPrice", "Unit"), key = "unit", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "ItemPrice", "SalePrice"), key = "sale_price", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "ItemPrice", "SalePrice"), key = "sale_price", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() { header = ls.Get(Modules.CORE, "ItemPrice", "MinQty"), key = "min_qty", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = ls.Get(Modules.CORE, "ItemPrice", "EffectiveStartdate"), key = "effective_startdate", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }

        //todo: exprot excel get by code
    }
}
