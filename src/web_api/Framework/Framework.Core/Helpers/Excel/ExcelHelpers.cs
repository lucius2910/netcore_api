using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Framework.Core.Helpers.Excel;
using System.Reflection;

namespace Framework.Core.Helpers
{       
    public static class ExcelHelpers
    {
        public static async Task<byte[]> Export<T>(List<T> data, List<ExcelItem> cellConfigs)
        {
            if (cellConfigs == null || cellConfigs.Count == 0)
                return null;

            using (var ms = new MemoryStream())
            {
                using (var document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
                {
                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = document.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet();

                    // Add a WorkbookStylesPart to the WorkbookPart.
                    WorkbookStylesPart workStylePart = workbookpart.AddNewPart<WorkbookStylesPart>();
                    workStylePart.Stylesheet = new Stylesheet();

                    // Add Sheets to the Workbook.
                    Sheets sheets = workbookpart.Workbook.AppendChild(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    Sheet sheet = new Sheet()
                    {
                        Id = workbookpart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "mySheet"
                    };
                    sheets.Append(sheet);

                    // Append row headder
                    Row workRow = new Row();
                    foreach (var item in cellConfigs)
                    {
                        Cell cellHeader = CreateCell(item.header, item.type);
                        cellHeader.AddBorder(workbookpart);
                        //cellHeader.AddFill();
                        //cellHeader.AddAlign();
                        workRow.Append(cellHeader);
                    }

                    SheetData sheetData = new SheetData();
                    sheetData.Append(workRow);

                    // Append data
                    foreach (var dataItem in data)
                    {
                        Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                        sheetData.Append(partsRows);
                    }

                    worksheetPart.Worksheet.Append(sheetData);

                    // Close the document.
                    document.Close();

                    return ms.ToArray();
                }
            }
        }

        #region Private method
        private static Row GenerateRowContent<T>(T entity, List<ExcelItem> cellConfigs)
        {
            Row tRow = new Row();
            Type entType = entity.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(entType.GetProperties());
            foreach (var itemConfig in cellConfigs)
            {
                var itemProp = props.Where(x => x.Name == itemConfig.key).First();
                var propValue = itemProp.GetValue(entity);
                propValue = (propValue != null) ? propValue : "";
                Cell c = CreateCell(propValue.ToString(), itemConfig.type);
                tRow.AppendChild(c);
            }
            return tRow;
        }

        private static void SetBorderAndFill(WorkbookPart workbookPart, WorksheetPart workSheetPart ,Cell cell, bool isHeader)
        {
            CellFormat cellFormat = cell.StyleIndex != null ? GetCellFormat(workbookPart, cell.StyleIndex).CloneNode(true) as CellFormat : new CellFormat();

            //cellFormat.BorderId = InsertBorder(workbookPart, GenerateBorder());
            cell.StyleIndex = InsertCellFormat(workbookPart, cellFormat);
            if (isHeader)
                cellFormat.FillId = InsertFill(workbookPart, GenerateFill());
        }

        private static CellFormat GetCellFormat(WorkbookPart workbookPart, uint styleIndex)
        {
            if (workbookPart.WorkbookStylesPart.Stylesheet.CellFormats != null)
                return workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First().Elements<CellFormat>().ElementAt((int)styleIndex);
            else
                return new CellFormat();
        }

        private static uint InsertCellFormat(WorkbookPart workbookPart, CellFormat cellFormat)
        {
            CellFormats cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats != null ? workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First() : new CellFormats();
            cellFormats.Append(cellFormat);
            return (uint)(cellFormats.Count == null ? 1 : cellFormats.Count++);
        }

        private static void AddBorder(this Cell cell, WorkbookPart workbookPart)
        {
            var border = GenerateBorder();
            CellFormat cellFormat = cell.StyleIndex != null ? GetCellFormat(workbookPart, cell.StyleIndex).CloneNode(true) as CellFormat : new CellFormat();

            Borders borders = workbookPart.WorkbookStylesPart.Stylesheet.Borders != null ? workbookPart.WorkbookStylesPart.Stylesheet.Elements<Borders>().First() : new Borders();
            borders.Append(border);
            cellFormat.BorderId = UInt32Value.FromUInt32((uint)(borders.Descendants<Border>().Count() - 1));

        }

        private static uint InsertFill(WorkbookPart workbookPart, Fill fill)
        {
            Fills fills = workbookPart.WorkbookStylesPart.Stylesheet.Fills != null ? workbookPart.WorkbookStylesPart.Stylesheet.Elements<Fills>().First() : new Fills();
            fills.Append(fill);
            return (uint)(fills.Count == null ? 1 : fills.Count++);
        }

        private static Border GenerateBorder()
        {
            var border = new Border();
            //Color borderColor = new Color() { Indexed = (UInt32Value)64U };

            LeftBorder leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            //leftBorder.Append(borderColor);

            RightBorder rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            //rightBorder.Append(borderColor);

            TopBorder topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            //topBorder.Append(borderColor);

            BottomBorder bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            //bottomBorder.Append(borderColor);

            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);

            return border;
        }

        private static Fill GenerateFill()
        {
            Fill fill = new Fill();

            PatternFill patternFill = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "#6daad8" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };

            patternFill.Append(foregroundColor1);
            patternFill.Append(backgroundColor1);

            fill.Append(patternFill);

            return fill;
        }

        private static Cell CreateCell(string text, DataType? type)
        {
            Cell cell = new Cell();
            // TODO : format data | date, number, currency
            cell.CellValue = new CellValue(text);
            
            switch (type)
            {
                case DataType.NUMBER:
                    cell.DataType = CellValues.Number;
                    return cell;
                case DataType.DATE:
                case DataType.DATETIME:
                    cell.DataType = CellValues.Date;
                    return cell;
                default:
                    cell.DataType = CellValues.String;
                    return cell;
            }
        }
        #endregion
    }
}
