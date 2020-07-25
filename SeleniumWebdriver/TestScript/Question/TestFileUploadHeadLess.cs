using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriver.TestScript.Question
{
    [TestClass]
    public class TestFileUploadHeadLess
    {
        private readonly string URL = "https://cgi-lib.berkeley.edu/ex/fup.html";
        private readonly By UploadLocator = By.XPath("//input[@name='upfile']");
        private readonly By UploadButton = By.XPath("//input[@value='Press']");
        private readonly string FileLocation = @"C:\Data\log\Exception.txt";


        /// <summary>
        /// This script will do the file upload. But it will only work if the file upload control is of type=file
        /// <input type="file" name="upfile" />
        /// </summary>
        [TestMethod]
        public void TestUpload()
        {
            NavigationHelper.NavigateToUrl(URL);
            GenericHelper.WaitForWebElement(UploadButton, TimeSpan.FromSeconds(60));
            ObjectRepository.Driver.FindElement(UploadLocator).SendKeys(FileLocation);
            ObjectRepository.Driver.FindElement(UploadButton).Click();
            var wait = GenericHelper.GetWebdriverWait(TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.UrlContains("fup.cgi"));
        }

    }
}
