using DocumentFormat.OpenXml.Spreadsheet;

namespace Framework.Core.Helpers.Excel
{
    public class ExcelItem
    {
        public string key {get; set;}
        public string header { get; set; }
        public DataType? type { get; set; }
        public CellAlign? header_align { get; set; }
        public CellAlign? content_align { get; set; }
    }
}
