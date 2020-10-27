using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumWebdriver.Keyword.DataEngineUtil
{
    public class ExcelUtilityHelper : IDisposable
    {
        #region Constructor

        public ExcelUtilityHelper(string fileName)
            : this(new FileInfo(fileName)) { }

        public ExcelUtilityHelper(FileInfo info)
        {
            _package = new ExcelPackage(info);
        }

        #endregion

        #region Fields

        private static ExcelPackage _package;

        #endregion

        #region Public

        public List<string> GetAllSheetName()
        {
            return _package.Workbook.Worksheets.Select((worksheet => worksheet.Name)).ToList();
        }

        public int GetTotalSheetCount()
        {
            return _package.Workbook.Worksheets.Count;
        }

        public string GetCellValue(string sheetName, int row, int col)
        {
            return _package.Workbook.Worksheets[sheetName].Cells[row, col].Text;
        }

        public int GetTotalRows(string sheetName)
        {

            var count = 1;

            while (!GetCellValue(sheetName, count, 1).Equals(string.Empty))
            {
                count++;
            }

            return count;
        }

        public IDictionary<string, int> GetTestCaseRowNo(string sheet, int column = 1)
        {
            var totalRows = GetTotalRows(sheet);
            var testCaseId = new Dictionary<string, int>();

            for (var i = 2; i < totalRows; i++)
            {
                var celValue = GetCellValue(sheet, i, column);
                if (!testCaseId.ContainsKey(celValue))
                {
                    testCaseId.Add(celValue, i);
                }
            }
            return testCaseId;
        }

        public void SaveSheet(string fileName = "")
        {
            _package.Save("".Equals(fileName) ? null : fileName);
        }

        public void WriteToCell(string sheetName, int row, int column, string
           value)
        {

            _package.Workbook.Worksheets[sheetName].Cells[row, column].Value = value;
            _package.Workbook.Worksheets[sheetName].Cells[row, column].Style.Font.Bold = true;
            _package.Workbook.Worksheets[sheetName].Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
            _package.Workbook.Worksheets[sheetName].Cells[row, column].Style.Fill.BackgroundColor.SetColor(value.Equals("Fail",
                StringComparison.OrdinalIgnoreCase)
                ? Color.Red
                : Color.Green);
            _package.Workbook.Worksheets[sheetName].Cells[row, column].AutoFitColumns();
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            _package.Dispose();
        }

        #endregion

    }
}
