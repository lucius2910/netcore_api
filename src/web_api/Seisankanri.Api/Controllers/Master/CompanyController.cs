using Business.Core.Contracts;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Framework.Core.Helpers.Excel;
using Microsoft.AspNetCore.Mvc;
using Seisankanri.Api.Extensions;
using Seisankanri.Api.Filters;

namespace Seisankanri.Api.Controllers
{
    [Route("api/company")]
    [ApiController]
    [BAuthorize]
    public class CompanyController : ControllerBase
    {
        protected ICompanyServices companyServices { get; set; }
        protected ILocalizeServices ls { get; set; }

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
        public async Task<IActionResult> GetAll([FromQuery] CompanyPagedRequest request)
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
                return Ok(data.ToResponse(ls));
            else
                return BadRequest(new { code = ResponseCode.NotFound, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_007) });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_001) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001) });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_002) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_002) });
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
                return Ok(new { code = ResponseCode.Success, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_003) });
            else
                return BadRequest(new { code = ResponseCode.SystemError, message = ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_003) });
        }

        [HttpGet]
        [Route("export_excel")]
        public async Task<IActionResult> ExportExel([FromQuery] CompanyPagedRequest request, [FromQuery] string file_name)
        {
            var dataResponse = await companyServices.GetPaged(request);

            List<ExcelItem> excelItems = new List<ExcelItem>()
            {
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "Code"), key = "code", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "PlaceF"), key = "place_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "DestinationF"), key = "destination_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CustomerF"), key = "customer_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SupplierF"), key = "supplier_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "OutsourcerF"), key = "outsourcer_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "BranchF"), key = "branch_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "TranspostF"), key = "transpost_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "MakerF"), key = "maker_f", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "EmpCd"), key = "emp_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CompanyShortName"), key = "company_short_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CompanyName1"), key = "company_name1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CompanyName2"), key = "company_name2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "Kana"), key = "kana", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "PostalCd"), key = "postal_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "Address1"), key = "address1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "Address2"), key = "address2", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "TelNo"), key = "tel_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "ExtensionNo"), key = "extension_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "FaxNo"), key = "fax_no", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CustomerPresidentName"), key = "customer_president_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CustomerPersonName"), key = "customer_person_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SaEmpCd"), key = "sa_emp_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "CutoffDate1"), key = "cutoff_date1", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SaRdDiv"), key = "sa_rd_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SaTaxDiv"), key = "sa_tax_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SaTaxCalcDiv"), key = "sa_tax_calc_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "SaTaxRdDiv"), key = "sa_tax_rd_div", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "EmpMailAddress"), key = "emp_mail_address", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "EmpPhone"), key = "emp_phone", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "BankCd"), key = "bank_cd", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "BankAccountNumber"), key = "emp_mail_address", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT },
                new ExcelItem() {  header = await ls.GetAsync(Modules.CORE, "Company", "BankAccountName"), key = "bank_account_name", type = DataType.TEXT, header_align = CellAlign.CENTER, content_align = CellAlign.LEFT }
            };
            var fileStream = await ExcelHelpers.Export(dataResponse.data.ToList(), excelItems);

            return File(fileStream, "application/octetstream", file_name);
        }
    }
}
