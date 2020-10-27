using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriver.Keyword.DataEngineUtil
{
    public class DataEngineUtility
    {
        private readonly int _keywordCol;
        private readonly int _locatorTypeCol;
        private readonly int _locatorValueCol;
        private readonly int _parameterCol;
        private readonly int _resultCol;
        private readonly int _exceptionCol;

        public DataEngineUtility(int keywordCol = 3, int locatorTypeCol = 4, int locatorValueCol = 5, int parameterCol = 6, int resultCol = 7, int exceptionCol = 8)
        {
            _keywordCol = keywordCol;
            _locatorTypeCol = locatorTypeCol;
            _locatorValueCol = locatorValueCol;
            _parameterCol = parameterCol;
            _resultCol = resultCol;
            _exceptionCol = exceptionCol;
        }

        private By GetElementLocator(string locatorType, string locatorValue)
        {
            switch (locatorType)
            {
                case "ClassName":
                    return By.ClassName(locatorValue);
                case "CssSelector":
                    return By.CssSelector(locatorValue);
                case "Id":
                    return By.Id(locatorValue);
                case "PartialLinkText":
                    return By.PartialLinkText(locatorValue);
                case "Name":
                    return By.Name(locatorValue);
                case "XPath":
                    return By.XPath(locatorValue);
                case "TagName":
                    return By.TagName(locatorValue);
                default:
                    return By.Id(locatorValue);
            }
        }

        private void PerformAction(string keyword, string locatorType, string locatorValue, params string[] args)
        {
            switch (keyword)
            {
                case "Click":
                    ButtonHelper.ClickButton(GetElementLocator(locatorType, locatorValue));
                    break;
                case "SendKeys":
                    TextBoxHelper.TypeInTextBox(GetElementLocator(locatorType, locatorValue), args[0]);
                    break;
                case "Select":
                    ComboBoxHelper.SelectElementByValue(GetElementLocator(locatorType, locatorValue), args[0]);
                    break;
                case "WaitForEle":
                    GenericHelper.WaitForWebElementInPage(GetElementLocator(locatorType, locatorValue),
                        TimeSpan.FromSeconds(50));
                    break;
                case "Navigate":
                    NavigationHelper.NavigateToUrl(args[0]);
                    break;
                default:
                    throw new NoSuchKeywordFoundException("Keyword Not Found : " + keyword);
            }
        }



        public void ExecuteScript(string xlPath, string sheetName, string childSheet)
        {
            using (var excelUtility = new ExcelUtilityHelper(xlPath))
            {
                var totalRow = excelUtility.GetTotalRows(sheetName);
                var testCaseId = excelUtility.GetTestCaseRowNo(childSheet);
                for (var i = 2; i < totalRow; i++)
                {
                    try
                    {
                        if (excelUtility.GetCellValue(sheetName, i, 1).Equals(string.Empty))
                            break;

                        if (!"Yes".Equals(excelUtility.GetCellValue(sheetName, i, 4), StringComparison.OrdinalIgnoreCase))
                            continue;
                        var tcIdCell = excelUtility.GetCellValue(sheetName, i, 3);
                        var tcIdIndex = testCaseId[tcIdCell];
                        ExecuteScript(excelUtility, childSheet, tcIdCell, tcIdIndex);
                        excelUtility.WriteToCell(sheetName, i, 5, "Pass");
                    }
                    catch (Exception)
                    {
                        excelUtility.WriteToCell(sheetName, i, 5, "Fail");
                    }
                }
                excelUtility.SaveSheet();
            }
        }



        public void ExecuteScript(ExcelUtilityHelper excelUtility, string sheetName, string tcId, int tcIdIndex)
        {
            var i = tcIdIndex;
            while (excelUtility.GetCellValue(sheetName, i, 1).Contains(tcId))
            {
                try
                {
                    if (string.Empty.Equals(excelUtility.GetCellValue(sheetName, i, _keywordCol)))
                        break;

                    var lctType = excelUtility.GetCellValue(sheetName, i, _locatorTypeCol);
                    var lctValue = excelUtility.GetCellValue(sheetName, i, _locatorValueCol);
                    var keyword = excelUtility.GetCellValue(sheetName, i, _keywordCol);
                    var param = excelUtility.GetCellValue(sheetName, i, _parameterCol);

                    PerformAction(keyword, lctType, lctValue, param);
                    excelUtility.WriteToCell(sheetName, i, _resultCol, "Pass");

                }
                catch (Exception ex)
                {
                    excelUtility.WriteToCell(sheetName, i, _resultCol, "Fail");
                    excelUtility.WriteToCell(sheetName, i, _exceptionCol, ex.Message);
                    throw;
                }
                i++;
            }

        }
    }
}
