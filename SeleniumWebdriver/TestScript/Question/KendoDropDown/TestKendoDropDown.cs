using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumWebdriver.TestScript.Question.KendoDropDown
{
    [TestClass]
    public class TestKendoDropDown
    {
        private const string DropDownXpath = "//div[@id='cap-view']//span[@aria-labelledby='color_label']//span[@aria-label='select']";
        private const string ItemXpath = "//li[text()='Grey']";

        [TestMethod]
        public void TestKenDropDown()
        {
            NavigationHelper.NavigateToUrl("https://demos.telerik.com/kendo-ui/dropdownlist/index");
            GenericHelper.WaitForWebElement(By.XPath(DropDownXpath), TimeSpan.FromSeconds(60));
            JavaScriptExecutor.ScrollIntoViewAndClick(By.XPath(DropDownXpath));
            GenericHelper.WaitForWebElement(By.XPath(ItemXpath), TimeSpan.FromSeconds(60));
            ButtonHelper.ClickButton(By.XPath(ItemXpath));
            Thread.Sleep(1000);
        }
    }
}
