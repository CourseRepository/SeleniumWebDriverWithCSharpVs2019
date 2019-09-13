using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriver.Settings;

namespace SeleniumWebdriver.ComponentHelper
{
   public class GridHelper
    {
       internal static string GetTableXpath(string locator, int row, int col)
       {
           return $"{locator}//tbody//tr[{row}]//td[{col}]";
       }

       private static IWebElement GetGridElement(string locator, int row, int col)
       {
           var xpath = GetTableXpath(locator, row, col);
           if (GenericHelper.IsElemetPresent(By.XPath(xpath + "//a")))
           {
               return ObjectRepository.Driver.FindElement(By.XPath(xpath + "//a"));
           }
           else if (GenericHelper.IsElemetPresent(By.XPath(xpath + "//input")))
           {
                return ObjectRepository.Driver.FindElement(By.XPath(xpath + "//input"));
           }
           else
           {
               return ObjectRepository.Driver.FindElement(By.XPath(xpath));
           }
       }

       public static string GetColumnValue(string @locator,int @row,int @col)
       {
          /* string tableXpath = GetTableXpath(locator, row, col);
           string value = string.Empty;


           if (GenericHelper.IsElemetPresent(By.XPath(tableXpath)))
           {
               value = ObjectRepository.Driver.FindElement(By.XPath(tableXpath)).Text;
           }

           return value;*/
           return GetGridElement(locator, row, col).Text;
       }

       public static IList<string> GetAllValues(string @locator)
       {
            List<string> list = new List<string>();

            var row = 1;
           var col = 1;

           while (GenericHelper.IsElemetPresent(By.XPath(GetTableXpath(locator,row,col))))
           {
                while (GenericHelper.IsElemetPresent(By.XPath(GetTableXpath(locator, row, col))))
                {
                    list.Add(ObjectRepository.Driver.FindElement(By.XPath(GetTableXpath(locator, row, col))).Text);
                    col++;
                }
               row++;
               col = 1;
           }
           return list;

       }

       public static void ClickButtonInGrid(string @locator, int @row, int @col)
       {
           GetGridElement(locator,row,col).Click();
       }

    }
}
