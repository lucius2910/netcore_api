using Microsoft.AspNetCore.Mvc;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Application.Common.Abstractions;
using Application.Common.Extensions;
using Framework.Core.Collections;
using WebAPI.Filters;
using Framework.Core.Helpers.Excel;

namespace WebAPI.Controllers.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/company")]
    [ApiController]
    [BAuthorize]
    public class CompanyController : ControllerBase
    {
        private ICompanyServices companyServices { get; set; }
        private ILocalizeServices ls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_companyServices"></param>
        /// <param name="_ls"></param>
        public CompanyController(ICompanyServices _companyServices,
            ILocalizeServices _ls)
        {
            companyServices = _companyServices;
            ls = _ls;
        }


        /// <summary>
        /// Get list company
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
        {
            var data = await companyServices.GetPaged(request);
            return Ok(data);
        }


        /// <summary>
        /// Get list company by type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list_by_type")]
        public async Task<IActionResult> GetListByType([FromQuery] CompanyTypePagedRequest request)
        {
            var data = await companyServices.GetListByType(request);
            return Ok(data);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_by_id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await companyServices.GetById(id);
            if (data != null)
                return Ok(data.ToResponse());
            else
                return BadRequest(data.ToResponse());
        }


        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CompanyRequest request)
        {
            int count = await companyServices.Create(request);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_001) });
        }

        /// <summary>
        /// Update company by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyRequest request)
        {
            var count = await companyServices.Update(id, request);
            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_002) });
        }

        /// <summary>
        /// Delete company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var count = await companyServices.Delete(id);

            if (count >= 1)
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.Core, Screen.Message, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.Core, Screen.Message, MessageKey.E_003) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="file_name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export_excel")]
        public async Task<IActionResult> ExportExel([FromQuery] PagedRequest request, [FromQuery] string file_name)
        {
            var dataResponse = await companyServices.GetPaged(request);

            List<ExcelItem> excelItems = new List<ExcelItem>()
            {
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "Code"), key = "code", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "PlaceF"), key = "place_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "DestinationF"), key = "destination_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CustomerF"), key = "customer_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SupplierF"), key = "supplier_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "OutsourcerF"), key = "outsourcer_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "BranchF"), key = "branch_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "TranspostF"), key = "transpost_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "MakerF"), key = "maker_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "EmpCd"), key = "emp_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CompanyShortName"), key = "company_short_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CompanyName1"), key = "company_name1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CompanyName2"), key = "company_name2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "Kana"), key = "kana", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "PostalCd"), key = "postal_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "Address1"), key = "address1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "Address2"), key = "address2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "TelNo"), key = "tel_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "ExtensionNo"), key = "extension_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "FaxNo"), key = "fax_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CustomerPresidentName"), key = "customer_president_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CustomerPersonName"), key = "customer_person_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SaEmpCd"), key = "sa_emp_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "CutoffDate1"), key = "cutoff_date1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SaRdDiv"), key = "sa_rd_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SaTaxDiv"), key = "sa_tax_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SaTaxCalcDiv"), key = "sa_tax_calc_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "SaTaxRdDiv"), key = "sa_tax_rd_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "EmpMailAddress"), key = "emp_mail_address", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "EmpPhone"), key = "emp_phone", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "BankCd"), key = "bank_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "BankAccountNumber"), key = "emp_mail_address", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.Core, "Company", "BankAccountName"), key = "bank_account_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }
    }
}
