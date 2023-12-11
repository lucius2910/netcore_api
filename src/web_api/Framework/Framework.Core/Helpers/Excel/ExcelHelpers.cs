﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Framework.Core.Helpers.Excel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Framework.Core.Helpers
{
    public static class ExcelHelpers
    {
        public static void StreamToFile(this MemoryStream ms, string path)
        {
            ms.Seek(0, SeekOrigin.Begin);

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                ms.CopyTo(fs);
                fs.Flush();
            }
        }

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
            worksheetPart.Worksheet = new Worksheet(
                                        new SheetViews(new SheetView() { WorkbookViewId = 0, ShowGridLines = new BooleanValue(false) }),
                                        new SheetData()
                                        );

            // Add a WorkbookStylesPart to the WorkbookPart.
            WorkbookStylesPart workStylePart = workbookpart.AddNewPart<WorkbookStylesPart>();
            workStylePart.Stylesheet = DefaltStylesheetData();
            return document;
        }

        public static WorkbookPart CreateSheet(this WorkbookPart workbookpart, string sheetName)
        {
            workbookpart.Workbook.AppendChild<Sheets>(new Sheets());
            workbookpart.WorksheetParts.First().Worksheet = new Worksheet(
                                        new SheetProperties(new PageSetupProperties() { FitToPage = BooleanValue.FromBoolean(true) }),
                                        new SheetViews(new SheetView() { WorkbookViewId = 0, ShowGridLines = new BooleanValue(false) }),
                                        new SheetData()
                                        );
            
            workbookpart.WorksheetParts.First().Worksheet.Save();
            Sheets sheets = workbookpart.Workbook.GetFirstChild<Sheets>();
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            string relationshipId = workbookpart.GetIdOfPart(worksheetPart);

            worksheetPart.AddPagePrint();

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

        public static WorksheetPart AddPagePrint(this WorksheetPart worksheetPart)
        {
            PageSetup pageSetup = new PageSetup();
            pageSetup.Orientation = OrientationValues.Portrait;
            pageSetup.Scale = 100;
            pageSetup.FitToWidth = 1;
            pageSetup.FitToHeight = 0;
            pageSetup.PaperSize = 9; // xlPaperA4 - A4 (210 mm x 297 mm)
            worksheetPart.Worksheet.AppendChild(pageSetup);

            return worksheetPart;
        }

        public static Worksheet AddPageSetupProperties(this Worksheet workheet)
        {
            workheet.SheetProperties = new SheetProperties(new PageSetupProperties() { FitToPage = BooleanValue.FromBoolean(true) });
            return workheet;
        }

        public static WorkbookPart FillCellInfomation(this WorkbookPart workbookpart, Dictionary<string, object> cellInfo)
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
                var dataObject = item.Value.GetType().GetProperties();
                // Insert the text into the SharedStringTablePart.
                var dataValue = dataObject[0].GetValue(item.Value, null).ToString();
                InsertSharedStringItem(dataValue, shareStringPart);
                uint indexValue = uint.Parse(Regex.Replace(item.Key, @"\D", ""));
                string cellLName = Regex.Replace(item.Key, @"\d", "");
                Cell cell = InsertCellInWorksheet(cellLName, indexValue, worksheetPart);
                var dataType = (DataType)dataObject[1].GetValue(item.Value, null);
                switch (dataType)
                {
                    case DataType.NUMBER:
                        cell.DataType = CellValues.Number;

                        break;
                    case DataType.DATE:
                        cell.DataType = CellValues.Date;

                        break;
                    case DataType.DATETIME:
                        cell.DataType = CellValues.Date;

                        break;
                    case DataType.MONTH:
                        cell.DataType = CellValues.Date;
                        break;
                    default:
                        cell.DataType = CellValues.String;
                        break;
                }
                cell.CellValue = new CellValue(dataValue);

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
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();
            sheetData.Append(workRow);

            // Append data
            foreach (var dataItem in data)
            {
                Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                sheetData.Append(partsRows);
            }
            //get your columns (where your width is set)
            Columns columns = AutoSize(sheetData, cellConfigs);
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            worksheetPart.Worksheet.InsertBefore(columns, sheetData);
            worksheetPart.Worksheet.Save();

            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            //workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Equals(sheetData);
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart FillGridData<T>(this WorkbookPart workbookpart, List<T> data, List<ExcelItem> cellConfigs, string cellStart)
        {
            // Append row headder
            Row workRow = new Row();

            foreach (var item in cellConfigs)
            {
                Cell cellHeader = CreateCell(item.header, item.type, true, false);
                workRow.Append(cellHeader);
            }

            // TODO: Find by sheet name
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();
            sheetData.Append(workRow);

            // Append data
            foreach (var dataItem in data)
            {
                Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                sheetData.Append(partsRows);
            }
            //get your columns (where your width is set)
            Columns columns = AutoSize(sheetData, cellConfigs);
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            worksheetPart.Worksheet.InsertBefore(columns, sheetData);
            worksheetPart.Worksheet.Save();

            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            //workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Equals(sheetData);
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart MergeCell(this WorkbookPart workbookpart, string cellReference)
        {
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();

            MergeCells mergeCells = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<MergeCells>();

            if (mergeCells == null)
            { 
                mergeCells = new MergeCells();
                mergeCells.Append(new MergeCell() { Reference = new StringValue(cellReference) });
                workbookpart.WorksheetParts.First().Worksheet.InsertAfter(mergeCells, sheetData); 
            }
            else
            {
                mergeCells.Append(new MergeCell() { Reference = new StringValue(cellReference) });
            }

            workbookpart.WorksheetParts.First().Worksheet.Save();
            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart InsertCell(this WorkbookPart workbookpart, string cellReference, string value, DataType dataType, VCellStyle? cellStyle = VCellStyle.Normal)
        {
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();

            uint rowIndex = (uint)GetRowIndex(cellReference);

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            var rows = sheetData.Elements<Row>();
            if (sheetData.Elements<Row>().Where(r => r.RowIndex != null && r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex != null && r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).Count() > 0)
            {
                //return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
                return workbookpart;
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = CreateCell(value, dataType, false, false, cellStyle);
                newCell.CellReference = cellReference;
                row.AppendChild(newCell);

            }

            workbookpart.WorksheetParts.First().Worksheet.Save();
            workbookpart.WorkbookStylesPart.Stylesheet.Save();
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

        public static bool SetBorder(this WorkbookPart workbookPart, string sheetName, string cellReference, bool top = false, bool bottom = false, bool left = false, bool right = false, BorderStyleValues style = BorderStyleValues.Thin)
        {
            InsertCellBySheetName(workbookPart, cellReference, "", DataType.TEXT, sheetName, VCellStyle.TableText);

            //var cell = GetCellByAddress(workbookPart, sheetName, cellReference);
            //if (cell == null)
            //{
                
            //    cell = GetCellByAddress(workbookPart, sheetName, cellReference);
            //}
            //Stylesheet stylesheet = workbookPart.WorkbookStylesPart.Stylesheet;
            //if (cell.StyleIndex == null)
            //{
            //    var border = GenerateBorder(true, true, false, false, BorderStyleValues.Dotted);
            //    cell.StyleIndex = GennerateFormatCell(stylesheet, null, null, border);
            //}
            //else
            //{
            //    var cellFormat = stylesheet.CellFormats.Elements<CellFormat>().ElementAt(int.Parse(cell.StyleIndex));
            //    int idx = cellFormat.BorderId > 0 ? int.Parse(cellFormat.BorderId) : 0;
            //    var border = stylesheet.Borders.Elements<Border>().ElementAt(idx);
            //    cell.StyleIndex = GennerateFormatCell(stylesheet, null, null, border, cellFormat);
            //}
            return true;
        }

        public static WorkbookPart FillGridData<T>(this WorkbookPart workbookpart, List<T> data, List<ExcelItem> cellConfigs, bool showHeader = true)
        {
            // Append row header
            Row workRow = new Row();
            if (showHeader)
            {
                foreach (var item in cellConfigs)
                {
                    Cell cellHeader = CreateCell(item.header, item.type, true, false);
                    workRow.Append(cellHeader);
                }
            }

            // TODO: Find by sheet name
            SheetData sheetData = workbookpart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();
            sheetData.Append(workRow);

            // Append data
            foreach (var dataItem in data)
            {
                Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                sheetData.Append(partsRows);
            }
            //get your columns (where your width is set)
            Columns columns = AutoSize(sheetData, cellConfigs);
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            worksheetPart.Worksheet.InsertBefore(columns, sheetData);
            worksheetPart.Worksheet.Save();

            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            //workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Equals(sheetData);
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart InsertCellBySheetName(this WorkbookPart workbookpart, string cellReference, string value, DataType dataType, string name_sheet = null, VCellStyle? cellStyle = VCellStyle.Normal)
        {
            WorksheetPart worksheetPart;
            if (name_sheet != null)
            {
                var sheet = workbookpart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == name_sheet);
                if (sheet == null)
                {
                    // Sheet with the specified name does not exist
                    throw new ArgumentException($"Sheet with name '{name_sheet}' does not exist.");
                }

                worksheetPart = (WorksheetPart)workbookpart.GetPartById(sheet.Id);
            }
            else
            {
                worksheetPart = workbookpart.WorksheetParts.First();
            }
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            uint rowIndex = (uint)GetRowIndex(cellReference);

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            var rows = sheetData.Elements<Row>();
            if (sheetData.Elements<Row>().Where(r => r.RowIndex != null && r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex != null && r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).Count() > 0)
            {
                Cell existCell = row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
                existCell.CellValue = new CellValue(value);
                existCell.DataType = CellValues.String;
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
                    else if (cell.CellReference.Value.Length >= cellReference.Length)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = CreateCell(value, dataType, false, false, cellStyle);
                newCell.CellReference = cellReference;
                if(refCell != null)
                    row.InsertBefore(newCell, refCell);
                else
                    row.AppendChild(newCell);
            }

            workbookpart.WorksheetParts.First().Worksheet.Save();
            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            workbookpart.Workbook.Save();
            return workbookpart;

        }

        public static WorkbookPart MergeCellBySheetName(this WorkbookPart workbookpart, string cellReference, string name_sheet = null, Boolean boder = false)
        {
            WorksheetPart worksheetPart;

            if (name_sheet != null)
            {
                var sheet = workbookpart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == name_sheet);
                if (sheet == null)
                {
                    // Sheet with the specified name does not exist
                    throw new ArgumentException($"Sheet with name '{name_sheet}' does not exist.");
                }

                worksheetPart = (WorksheetPart)workbookpart.GetPartById(sheet.Id);
            }
            else
            {
                worksheetPart = workbookpart.WorksheetParts.First();
            }
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            MergeCells mergeCells = worksheetPart.Worksheet.GetFirstChild<MergeCells>();

            if (mergeCells == null)
            {
                mergeCells = new MergeCells();
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);

                mergeCells.Append(new MergeCell() { Reference = new StringValue(cellReference) });
            }
            else
            {
                mergeCells.Append(new MergeCell() { Reference = new StringValue(cellReference) });

            }


            workbookpart.WorksheetParts.First().Worksheet.Save();
            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart FillGridDataBySheetName<T>(this WorkbookPart workbookpart, List<T> data, List<ExcelItem> cellConfigs, string name_sheet = null)
        {
            // Append row headder
            Row workRow = new Row();

            foreach (var item in cellConfigs)
            {
                Cell cellHeader = CreateCell(item.header, item.type, true, false);
                workRow.Append(cellHeader);
            }

            // TODO: Find by sheet name
            WorksheetPart WorksheetPart;
            if (name_sheet != null)
            {
                var sheet = workbookpart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == name_sheet);
                if (sheet == null)
                {
                    // Sheet with the specified name does not exist
                    throw new ArgumentException($"Sheet with name '{name_sheet}' does not exist.");
                }

                WorksheetPart = (WorksheetPart)workbookpart.GetPartById(sheet.Id);
            }
            else
            {
                WorksheetPart = workbookpart.WorksheetParts.First();
            }
            SheetData sheetData = WorksheetPart.Worksheet.GetFirstChild<SheetData>();
            sheetData.Append(workRow);

            // Append data
            foreach (var dataItem in data)
            {
                Row partsRows = GenerateRowContent(dataItem, cellConfigs);
                sheetData.Append(partsRows);
            }
            //get your columns (where your width is set)
            Columns columns = AutoSize(sheetData, cellConfigs);
            WorksheetPart worksheetPart = workbookpart.WorksheetParts.First();
            sheetData.InsertBeforeSelf(columns);
            worksheetPart.Worksheet.Save();

            workbookpart.WorkbookStylesPart.Stylesheet.Save();
            //workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Equals(sheetData);
            workbookpart.Workbook.Save();
            return workbookpart;
        }

        public static WorkbookPart SetRowHeight(this WorkbookPart workbookPart, uint rowIndex, double rowHeight, string name_sheet = null)
        {
            WorksheetPart worksheetPart;
            if (name_sheet != null)
            {
                var sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == name_sheet);
                if (sheet == null)
                {
                    // Sheet with the specified name does not exist
                    throw new ArgumentException($"Sheet with name '{name_sheet}' does not exist.");
                }

                worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            }
            else
            {
                worksheetPart = workbookPart.WorksheetParts.First();
            }
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);

            if (row != null)
            {
                row.CustomHeight = true;
                row.Height = new DoubleValue(rowHeight);
            }
            else
            {
                row = new Row() { RowIndex = rowIndex, Height = new DoubleValue(rowHeight) };
                sheetData.Append(row);
            }


            return workbookPart;
        }

        public static string GenerateColumnLetter(int n, int startIndex = 65)
        {
            string name = "";
            n += startIndex - 65;

            while (n > 0)
            {
                n--;
                name = (char)((n % 26) + 65) + name;
                n /= 26;
            }

            return name;
        }

        #region Private method
        private static uint? GetColumnIndex(string cellReference)
        {
            if (string.IsNullOrEmpty(cellReference))
            {
                return null;
            }

            //remove digits
            string columnReference = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);

            int columnNumber = -1;
            int mulitplier = 1;

            //working from the end of the letters take the ASCII code less 64 (so A = 1, B =2...etc)
            //then multiply that number by our multiplier (which starts at 1)
            //multiply our multiplier by 26 as there are 26 letters
            foreach (char c in columnReference.ToCharArray().Reverse())
            {
                columnNumber += mulitplier * ((int)c - 64);

                mulitplier = mulitplier * 26;
            }

            //the result is zero based so return columnnumber + 1 for a 1 based answer
            //this will match Excel's COLUMN function
            return (uint)columnNumber + 1;
        }

        private static uint? GetRowIndex(string cellReference)
        {
            return (uint)int.Parse(string.Join("", new Regex("[0-9]").Matches(cellReference)));
        }

        private static Cell CreateCell(string value, string columnName, int rowIndex)
        {
            string cellReference = columnName + rowIndex.ToString();

            Cell cell = new Cell()
            {
                CellReference = cellReference,
                CellValue = new CellValue(value),
                DataType = CellValues.String
            };

            return cell;
        }

        private static Cell GetCellByAddress(this WorkbookPart workbookpart, string index)
        {
            var sheetData = workbookpart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
            var cell = sheetData.ChildElements.SelectMany(x => x.Elements<Cell>()).Where(c => c.CellReference == index).FirstOrDefault();
            return cell;
        }

        private static Cell GetCellByAddress(this WorkbookPart workbookpart, string sheetName, string cellReference)
        {
            WorksheetPart worksheetPart;

            if (sheetName != null)
            {
                var sheet = workbookpart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);
                if (sheet == null)
                {
                    // Sheet with the specified name does not exist
                    throw new ArgumentException($"Sheet with name '{sheetName}' does not exist.");
                }

                worksheetPart = (WorksheetPart)workbookpart.GetPartById(sheet.Id);
            }
            else
            {
                worksheetPart = workbookpart.WorksheetParts.First();
            }

            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            var cell = sheetData.ChildElements.SelectMany(x => x.Elements<Cell>()).Where(c => c.CellReference == cellReference).FirstOrDefault();
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
                    var fontCount = (uint)stylesheet.Fonts.ChildElements.Count() - 1;
                    assign.FontId = fontCount > 0 ? fontCount : 0;
                }
                if (fill != null)
                {
                    stylesheet.Fills.ToList()[int.Parse(assign.FillId)].Equals(fill);
                    var fillCount = (uint)stylesheet.Fills.ChildElements.Count() - 1;
                    assign.FillId = fillCount > 0 ? fillCount : 0;
                }
                if (border != null)
                {
                    var borderCount = (uint)stylesheet.Borders.ChildElements.Count() - 1;
                    stylesheet.Borders.ToList()[int.Parse(assign.BorderId)].Equals(border);
                    assign.BorderId = borderCount > 0 ? borderCount : 0;
                }
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
            var listRowIndex = sheetData.Elements<Row>().Where(r => r.RowIndex != null && r.RowIndex == rowIndex).ToList();
            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;

            if (listRowIndex.Count() != 0)
            {
                row = listRowIndex.First();
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
            if (entType.Name == "ExpandoObject")
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
            else
            {
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
        private static Alignment GenerateAlignment(HorizontalAlignmentValues values, bool wrapText = false)
        {
            return new Alignment()
            {
                Horizontal = values,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = wrapText
            };
        }

        private static Cell CreateCell(string text, DataType? type, bool is_table_header = false, bool is_table_cell = false, VCellStyle? cellStyle = VCellStyle.Normal)
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

                case DataType.DECIMAL:
                    cell.DataType = CellValues.Number;
                    cell.CellValue = new CellValue(text);
                    if (is_table_cell)
                        cell.StyleIndex = 5;

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
                    if (cellStyle != VCellStyle.Normal)
                        cell.StyleIndex = UInt32Value.FromUInt32((uint)cellStyle);
                    else if (is_table_cell)
                        cell.StyleIndex = 2;
                    break;
            }

            return cell;
        }

        /// <summary>
        /// 0: Normal cell
        /// 1: Table header
        /// 2: Table cell text
        /// 3: Table cell number
        /// 4: Table cell date
        /// 5: Table cell date time
        /// 6: Template title
        /// 7: Alight center
        /// 8: Border Alight left
        /// 9: Alight right
        /// 10: Border Alight center
        /// </summary>
        /// <returns></returns>
        private static Stylesheet DefaltStylesheetData()
        {
            Stylesheet ss = new Stylesheet();

            //var fontFamily = "ＭＳ 明朝";
            var fontFamily = "ＭＳ Ｐゴシック";
            Fonts fts = new Fonts(
                // Cell normal
                new Font(
                    new FontSize() { Val = 11 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = fontFamily }),
                // Table header
                new Font(
                    new Bold(),
                    new FontSize() { Val = 11 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = fontFamily }),
                 // Page header
                 new Font(
                    new Bold(),
                    new FontSize() { Val = 14 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = fontFamily })
            );
            ss.Append(fts);

            Fills fills = new Fills(
                new Fill(new PatternFill() { PatternType = PatternValues.Solid, BackgroundColor = new BackgroundColor() { } }), // Don't remove, it excel default
                new Fill(new PatternFill() { PatternType = PatternValues.Solid, BackgroundColor = new BackgroundColor() { } }), // Don't remove, it excel default 
                new Fill(new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "E8E8E8" } }) { PatternType = PatternValues.Solid }) // start from FillId = 2
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
                    new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
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
                new CellFormat(new Alignment() { WrapText = false }) { FontId = 0, FillId = 0, BorderId = 1 },
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center }) { FontId = 1, FillId = 2, BorderId = 1, Alignment = GenerateAlignment(HorizontalAlignmentValues.Center), ApplyFont = true, ApplyFill = true, ApplyBorder = true, ApplyAlignment = true },
                new CellFormat(new Alignment() { WrapText = false }) { FontId = 0, FillId = 0, BorderId = 2, ApplyFont = true, ApplyBorder = true }, // table text cell
                new CellFormat(new Alignment() { WrapText = false }) { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 37, ApplyNumberFormat = true, ApplyFont = true, ApplyBorder = true }, // table number cell
                new CellFormat(new Alignment() { WrapText = false }) { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 14, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table date cell
                new CellFormat(new Alignment() { WrapText = false }) { FontId = 0, FillId = 0, BorderId = 2, NumberFormatId = 0, ApplyNumberFormat = true, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // table date cell
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center }) { FontId = 2, FillId = 0, BorderId = 0, Alignment = GenerateAlignment(HorizontalAlignmentValues.Center), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true }, // template title
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center }) { FontId = 0, FillId = 0, BorderId = 0, Alignment = GenerateAlignment(HorizontalAlignmentValues.Center), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true }, // template title not bold
                new CellFormat(new Alignment() { WrapText = true, Horizontal = HorizontalAlignmentValues.Left }) { FontId = 0, FillId = 0, BorderId = 2, Alignment = GenerateAlignment(HorizontalAlignmentValues.Left, true), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true }, // template title Alignment Right
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Right }) { FontId = 0, FillId = 0, BorderId = 0, Alignment = GenerateAlignment(HorizontalAlignmentValues.Right), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true }, // template title Alignment Right
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }) { FontId = 0, FillId = 0, BorderId = 2, Alignment = GenerateAlignment(HorizontalAlignmentValues.Center), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true }, // Border Alignment center
                new CellFormat(new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Left }) { FontId = 2, FillId = 0, BorderId = 0, Alignment = GenerateAlignment(HorizontalAlignmentValues.Left), ApplyFont = true, ApplyBorder = true, ApplyAlignment = true } // template title

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

        private static Columns AutoSize(SheetData sheetData, List<ExcelItem> colConfigs)
        {
            var maxColWidth = GetMaxCharacterWidth(sheetData);

            Columns columns = new Columns();
            //this is the width of my font - yours may be different
            double maxWidth = 7;
            foreach (var item in maxColWidth)
            {
                //width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256
                double width = colConfigs[item.Key].width ?? 10; // Math.Truncate((item.Value * maxWidth + 5) / maxWidth * 256) / 256;

                //pixels=Truncate(((256 * {width} + Truncate(128/{Maximum Digit Width}))/256)*{Maximum Digit Width})
                double pixels = Math.Truncate(((256 * width + Math.Truncate(128 / maxWidth)) / 256) * maxWidth);

                //character width=Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100
                double charWidth = Math.Truncate((pixels - 5) / maxWidth * 100 + 0.5) / 100;

                Column col = new Column() { BestFit = false, Min = (UInt32)(item.Key + 1), Max = (UInt32)(item.Key + 1), CustomWidth = true, Width = (DoubleValue)(width) };
                columns.Append(col);
            }

            return columns;
        }

        private static Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            Dictionary<int, int> maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            UInt32[] numberStyles = new UInt32[] { 5, 6, 7, 8 }; //styles that will add extra chars
            UInt32[] boldStyles = new UInt32[] { 1, 2, 3, 4, 6, 7, 8 }; //styles that will bold
            foreach (var r in rows)
            {
                var cells = r.Elements<Cell>().ToArray();

                //using cell index as my column
                for (int i = 0; i < cells.Length; i++)
                {
                    var cell = cells[i];
                    var cellValue = cell.CellValue == null ? string.Empty : cell.CellValue.InnerText;
                    var cellTextLength = cellValue.Length * 2;

                    if (cell.StyleIndex != null && numberStyles.Contains(cell.StyleIndex))
                    {
                        int thousandCount = (int)Math.Truncate((double)cellTextLength / 4);

                        //add 3 for '.00' 
                        cellTextLength += (3 + thousandCount);
                    }

                    if (cell.StyleIndex != null && boldStyles.Contains(cell.StyleIndex))
                    {
                        //add an extra char for bold - not 100% acurate but good enough for what i need.
                        cellTextLength += 1;
                    }

                    if (maxColWidth.ContainsKey(i))
                    {
                        var current = maxColWidth[i];
                        if (cellTextLength > current)
                        {
                            maxColWidth[i] = cellTextLength;
                        }
                    }
                    else
                    {
                        maxColWidth.Add(i, cellTextLength);
                    }
                }
            }

            return maxColWidth;
        }

        private static Row GenerateRowContent<T>(T entity, List<ExcelItem> cellConfigs, int rowIndex, int startColumn)
        {
            Row tRow = new Row() { RowIndex = (UInt32Value)(uint)rowIndex };

            Type entType = entity.GetType();
            if (entType.Name == "ExpandoObject")
            {
                IDictionary<string, object> propertyValues = (IDictionary<string, object>)entity;
                int columnIndex = startColumn;
                foreach (var itemConfig in cellConfigs)
                {
                    var propValue = propertyValues[itemConfig.key];
                    propValue = (propValue != null) ? propValue : "";
                    Cell c = CreateCell(propValue.ToString(), itemConfig.type, false, true);
                    c.CellReference = GetCellReference(columnIndex, rowIndex);
                    tRow.AppendChild(c);
                    columnIndex++;
                }
            }
            else
            {
                IList<PropertyInfo> props = new List<PropertyInfo>(entType.GetProperties());
                int columnIndex = startColumn;
                foreach (var itemConfig in cellConfigs)
                {
                    var itemProp = props.FirstOrDefault(x => x.Name == itemConfig.key);
                    if (itemProp != null)
                    {
                        var propValue = itemProp.GetValue(entity);
                        propValue = (propValue != null) ? propValue : "";
                        Cell c = CreateCell(propValue.ToString(), itemConfig.type, false, true);
                        c.CellReference = GetCellReference(columnIndex, rowIndex);
                        tRow.AppendChild(c);
                    }
                    columnIndex++;
                }
            }

            return tRow;
        }

        private static string GetCellReference(int columnIndex, int rowIndex)
        {
            string columnName = GetExcelColumnName(columnIndex);
            return $"{columnName}{rowIndex}";
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
        #endregion

    }

}
