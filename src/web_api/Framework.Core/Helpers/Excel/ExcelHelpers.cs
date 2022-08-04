using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Framework.Core.Helpers.Excel;
using System.Reflection;
using System.Text.RegularExpressions;

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
                        //cellHeader.AddBorder(workbookpart);
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

        public static SpreadsheetDocument CreateWorkbookPart(this SpreadsheetDocument document)
        {
            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet();

            // Add a WorkbookStylesPart to the WorkbookPart.
            WorkbookStylesPart workStylePart = workbookpart.AddNewPart<WorkbookStylesPart>();
            workStylePart.Stylesheet = DefaltStylesheetData();
            return document;
        }

        public static WorkbookPart CreateSheet(this WorkbookPart workbookpart, string sheetName)
        {
            workbookpart.Workbook.AppendChild<Sheets>(new Sheets());
            workbookpart.WorksheetParts.First().Worksheet = new Worksheet(new SheetData());
            workbookpart.WorksheetParts.First().Worksheet.Save();
            Sheets sheets = workbookpart.Workbook.GetFirstChild<Sheets>();
            WorksheetPart newWorksheetPart = workbookpart.WorksheetParts.First();
            string relationshipId = workbookpart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart FillCellInfomation(this WorkbookPart workbookpart, Dictionary<string, string> cellInfo)
        {
            SharedStringTablePart shareStringPart;
            if (workbookpart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
            {
                shareStringPart = workbookpart.GetPartsOfType<SharedStringTablePart>().First();
            }
            else
            {
                shareStringPart = workbookpart.AddNewPart<SharedStringTablePart>();
            }
            // Insert a new worksheet.
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            foreach (var item in cellInfo)
            {
                // Insert the text into the SharedStringTablePart.
                int index = InsertSharedStringItem(item.Value, shareStringPart);
                uint indexValue = uint.Parse(Regex.Replace(item.Key, @"\D", ""));
                string cellLName = Regex.Replace(item.Key, @"\d", "");
                Cell cell = InsertCellInWorksheet(cellLName, indexValue, worksheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
            // Save the new worksheet.
            worksheetPart.Worksheet.Save();
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart FillGridData<T>(this WorkbookPart workbookpart, List<T> data, List<ExcelItem> cellConfigs)
        {
            // Append row headder
            Row workRow = new Row();
            foreach (var item in cellConfigs)
            {
                Cell cellHeader = CreateCell(item.header, item.type, true, false);
                workRow.Append(cellHeader);
            }

            // TODO: Find by sheet name
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
            sheetData.Append(workRow);

            // Append data
            foreach (var dataItem in data)
            {
                Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                sheetData.Append(partsRows);
            }

            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Equals(sheetData);
            workbookpart.Workbook.Save();
            return workbookpart;
        }
        
        public static bool IsBold(this WorkbookPart workbookPart, string index)
        {

            var cell = GetCellByAddress(workbookPart, index);

            if (cell == null)
            {
                return false;
            }
            Stylesheet stylesheet = workbookPart.WorkbookStylesPart.Stylesheet;
            if (cell.StyleIndex == null)
            {
                var font = GenerateFont(true);
                cell.StyleIndex = GennerateFormatCell(stylesheet, font);
            }
            else
            {
                var cellFormat = stylesheet.CellFormats.Elements<CellFormat>()
                                          .ElementAt(int.Parse(cell.StyleIndex));
                var font = stylesheet.Fonts.Elements<Font>().ElementAt(int.Parse(cellFormat.FontId) - 1);
                cell.StyleIndex = GennerateFormatCell(stylesheet, font, null, null, cellFormat);

            }
            return true;
        }

        public static bool FillColor(this WorkbookPart workbookPart, string index, string color = "D1F2EB")
        {

            var cell = GetCellByAddress(workbookPart, index);

            if (cell == null)
            {
                return false;
            }
            Stylesheet stylesheet = workbookPart.WorkbookStylesPart.Stylesheet;
            if (cell.StyleIndex == null)
            {
                var fill = GenerateFill(color);
                cell.StyleIndex = GennerateFormatCell(stylesheet, null, fill);
            }
            else
            {
                var cellFormat = stylesheet.CellFormats.Elements<CellFormat>()
                                            .ElementAt(int.Parse(cell.StyleIndex));
                var fill = stylesheet.Fills.Elements<Fill>().ElementAt(int.Parse(cellFormat.FillId) - 1);
                cell.StyleIndex = GennerateFormatCell(stylesheet, null, fill, null, cellFormat);
            }
            return true;
        }

        public static bool SetBorder(this WorkbookPart workbookPart, string index, bool top = false, bool bottom = false, bool left = false, bool right = false, BorderStyleValues style = BorderStyleValues.Thin)
        {
            var cell = GetCellByAddress(workbookPart, index);
            if (cell == null)
            {
                return false;
            }
            Stylesheet stylesheet = workbookPart.WorkbookStylesPart.Stylesheet;
            if (cell.StyleIndex == null)
            {
                var border = GenerateBorder(true, true, false, false, BorderStyleValues.Dotted);
                cell.StyleIndex = GennerateFormatCell(stylesheet, null, null, border);
            }
            else
            {
                var cellFormat = stylesheet.CellFormats.Elements<CellFormat>().ElementAt(int.Parse(cell.StyleIndex));
                int idx = cellFormat.BorderId > 0 ? int.Parse(cellFormat.BorderId) : 0;
                var border = stylesheet.Borders.Elements<Border>().ElementAt(idx);
                cell.StyleIndex = GennerateFormatCell(stylesheet, null, null, border, cellFormat);
            }
            return true;
        }

        #region Private method

        private static Cell GetCellByAddress(this WorkbookPart workbookpart, string index)
        {
            var sheetData = workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
            var cell = sheetData.ChildElements.SelectMany(x => x.Elements<Cell>()).Where(c => c.CellReference == index).FirstOrDefault();
            return cell;
        }

        private static Font GenerateFont(bool is_bold, int? fontsize = 12, string? color = "#000")
        {
            Font font = new Font();
            if (is_bold)
            {
                font.Bold = new Bold();
            }
            font.FontSize = new FontSize() { Val = fontsize };
            font.Color = new Color() { Rgb = new HexBinaryValue() { Value = color } };
            return font;
        }

        private static Fill GenerateFill(string background)
        {
            Fill fill = new Fill();

            if (!string.IsNullOrEmpty(background))
            {
                fill.PatternFill = new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = new HexBinaryValue() { Value = background } } };
            }
            return fill;
        }

        private static Border GenerateBorder(bool top = false, bool bottom = false, bool left = false, bool right = false, BorderStyleValues style = BorderStyleValues.Thin)
        {
            Border border = new Border();
            if (top)
            {
                border.TopBorder = new TopBorder(new Color() { Auto = true })
                { Style = style };
            }
            if (bottom)
            {
                border.BottomBorder = new BottomBorder(new Color() { Auto = true })
                { Style = style };
            }
            if (left)
            {
                border.LeftBorder = new LeftBorder(new Color() { Auto = true })
                { Style = style };
            }
            if (right)
            {
                border.RightBorder = new RightBorder(new Color() { Auto = true })
                { Style = style };
            }
            return border;
        }

        private static uint? GennerateFormatCell(Stylesheet stylesheet, Font? font = null, Fill? fill = null, Border? border = null, CellFormat? assign = null)
        {
            if (assign == null)
            {
                CellFormat cellFormat = new CellFormat();
                if (font != null)
                {
                    stylesheet.Fonts.AppendChild(font);
                }
                if (fill != null)
                {
                    stylesheet.Fills.AppendChild(fill);

                }
                if (border != null)
                {
                    stylesheet.Borders.AppendChild(border);
                }
                var fontCount = (uint)stylesheet.Fonts.ChildElements.Count();
                var fillCount = (uint)stylesheet.Fills.ChildElements.Count();
                var borderCount = (uint)stylesheet.Borders.ChildElements.Count();
                cellFormat.FontId = fontCount > 0 && font != null ? fontCount - 1 : 0;
                cellFormat.FillId = fillCount > 0 && fill != null ? fillCount - 1 : 0;
                cellFormat.BorderId = borderCount > 0 && border != null ? borderCount - 1 : 0;
                stylesheet.CellFormats.Append(cellFormat);
            }
            else
            {
                if (font != null)
                {
                    stylesheet.Fonts.ToList()[int.Parse(assign.FontId)].Equals(font);
                }
                if (fill != null)
                {
                    stylesheet.Fills.ToList()[int.Parse(assign.FillId)].Equals(fill);
                }
                if (border != null)
                {
                    stylesheet.Borders.ToList()[int.Parse(assign.BorderId)].Equals(border);
                }
                var fontCount = (uint)stylesheet.Fonts.ChildElements.Count() - 1;
                var fillCount = (uint)stylesheet.Fills.ChildElements.Count() - 1;
                var borderCount = (uint)stylesheet.Borders.ChildElements.Count() - 1;
                assign.FontId = fontCount > 0 ? fontCount : 0;
                assign.FillId = fillCount > 0 ? fillCount : 0;
                assign.BorderId = borderCount > 0 ? borderCount : 0;
            }

            stylesheet.Save();
            return (uint)stylesheet.CellFormats.ChildElements.Count() - 1;
        }

        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        private static Row GenerateRowContent<T>(T entity, List<ExcelItem> cellConfigs)
        {
            Row tRow = new Row();
            Type entType = entity.GetType();
            if(entType.Name == "ExpandoObject")
            {
                IDictionary<string, object> propertyValues = (IDictionary<string, object>)entity;
                foreach (var itemConfig in cellConfigs)
                {
                    var propValue = propertyValues[itemConfig.key];
                    propValue = (propValue != null) ? propValue : "";
                    Cell c = CreateCell(propValue.ToString(), itemConfig.type, false, true);
                    tRow.AppendChild(c);
                }
            }
            else{
                IList<PropertyInfo> props = new List<PropertyInfo>(entType.GetProperties());
                foreach (var itemConfig in cellConfigs)
                {
                    var itemProp = props.Where(x => x.Name == itemConfig.key).First();
                    var propValue = itemProp.GetValue(entity);
                    propValue = (propValue != null) ? propValue : "";
                    Cell c = CreateCell(propValue.ToString(), itemConfig.type, false, true);
                    tRow.AppendChild(c);
                }
            }
            
            return tRow;
        }

        private static Cell CreateCell(string text, DataType? type, bool is_table_header = false, bool is_table_cell = false)
        {
            Cell cell = new Cell();
            

            if (is_table_header)
            {
                cell.CellValue = new CellValue(text);
                cell.DataType = CellValues.String;
                cell.StyleIndex = 1;
                return cell;
            }

            switch (type)
            {
                case DataType.NUMBER:
                    cell.DataType = CellValues.Number;
                    cell.CellValue = new CellValue(text);
                    if (is_table_cell)
                        cell.StyleIndex = 3;    
                    //else
                    //    cell.StyleIndex = 13;

                    break;
                case DataType.DATE:
                    cell.DataType = CellValues.Date;
                    cell.CellValue = new CellValue(text);
                    //if (is_table_cell)
                    //    cell.StyleIndex = 4;
                    //else
                    //    cell.StyleIndex = 10;

                    break;
                case DataType.DATETIME:
                    cell.DataType = CellValues.Date;
                    cell.CellValue = new CellValue(text);
                    //if (is_table_cell)
                    //    cell.StyleIndex = 5;
                    //else
                    //    cell.StyleIndex = 11;

                    break;
                case DataType.MONTH:
                    cell.DataType = CellValues.Date;
                    cell.CellValue = new CellValue(DateTime.Parse(text));
                    if (is_table_cell)
                        cell.StyleIndex = 4;
                    //else
                    //    cell.StyleIndex = 9;

                    break;
                default:
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(text);
                    if (is_table_cell)
                        cell.StyleIndex = 2;
                    break;
            }

            return cell;
        }

        /// <summary>
        /// 0: Normal cell
        /// 1: Table header
        /// 2: Table cell text
        /// 3: Table cell month
        /// 4: Table cell date
        /// 5: Table cell date time
        /// 6: Table cell time
        /// 7: Table cell number
        /// 8: Table cell number
        /// </summary>
        /// <returns></returns>
        private static Stylesheet DefaltStylesheetData()
        {
            Stylesheet ss = new Stylesheet();

            var fontFamily = "MS PGothic";
            Fonts fts = new Fonts(
                new Font(
                    new FontSize() { Val = 11 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = fontFamily }),
                new Font(
                    new Bold(),
                    new FontSize() { Val = 11 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "FFFFFF" } },
                    new FontName() { Val = fontFamily }),
                new Font(
                    new FontSize() { Val = 11 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "3F1111" } },
                    new FontName() { Val = fontFamily })
            );
            ss.Append(fts);

            Fills fills = new Fills(
                new Fill(new PatternFill() { PatternType = PatternValues.Solid, BackgroundColor = new BackgroundColor() { } }), // Don't remove, it excel default
                new Fill(new PatternFill() { PatternType = PatternValues.Solid, BackgroundColor = new BackgroundColor() { } }), // Don't remove, it excel default 
                new Fill(new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "006699" } }) { PatternType = PatternValues.Solid }) // start from FillId = 2
            );
            ss.Append(fills);

            Borders borders = new Borders(
                new Border(new LeftBorder(), new RightBorder(), new TopBorder(), new BottomBorder(), new DiagonalBorder()),
                new Border(
                    new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new DiagonalBorder()),
                new Border(
                    new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Dotted },
                    new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Dotted },
                    new DiagonalBorder())
            );
            ss.Append(borders);

            //ID Format Code
            //0   General
            //1   0
            //2   0.00
            //3   #,##0
            //4   #,##0.00
            //9   0 %
            //10  0.00 %
            //11  0.00E+00
            //12  # ?/?
            //13  # ??/??
            //14  d / m / yyyy
            //15  d - mmm - yy
            //16  d - mmm
            //17  mmm - yy
            //18  h: mm tt
            //19  h: mm: ss tt
            //20  H: mm
            //21  H: mm: ss
            //22  m / d / yyyy H: mm
            //37  #,##0 ;(#,##0)
            //38  #,##0 ;[Red](#,##0)
            //39  #,##0.00;(#,##0.00)
            //40  #,##0.00;[Red](#,##0.00)
            //45  mm: ss
            //46  [h]:mm: ss
            //47  mmss.0
            //48  ##0.0E+0
            //49  @

            //NumberingFormats nfs = new NumberingFormats(
            //    new NumberingFormat() { FormatCode = "mm/yyyy", NumberFormatId = 50 }
            //);
            //ss.Append(nfs);

            CellFormats csfs = new CellFormats(
                new CellFormat() { FontId = 0, FillId = 0, BorderId = 1 },
                new CellFormat() { FontId = 0, FillId = 2, BorderId = 1, ApplyFont = true, ApplyFill = true, ApplyBorder = true },
                new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table text cell
                new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 38, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table number cell
                new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 14, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true } // table date cell
                //new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 172, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table date time cell
                //new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 173, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table time cell
                //new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 174, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table number cell
                //new CellFormat() { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 175, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table number cell
                //new CellFormat() { FontId = 0, NumberFormatId = 170, ApplyNumberFormat = true, ApplyFont = true }, // normal month cell
                //new CellFormat() { FontId = 0, NumberFormatId = 171, ApplyNumberFormat = true, ApplyFont = true }, // normal date cell
                //new CellFormat() { FontId = 0, NumberFormatId = 172, ApplyNumberFormat = true, ApplyFont = true }, // normal date time cell
                //new CellFormat() { FontId = 0, NumberFormatId = 173, ApplyNumberFormat = true, ApplyFont = true }, // normal time cell
                //new CellFormat() { FontId = 0, NumberFormatId = 174, ApplyNumberFormat = true, ApplyFont = true }, // normal number cell
                //new CellFormat() { FontId = 0, NumberFormatId = 175, ApplyNumberFormat = true, ApplyFont = true } // normal number cell
            );

            ss.Append(csfs);

            return ss;
        }
        #endregion

    }
}
