using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumWebdriver.TestScript.Question
{
    [TestClass]
    public class TestIEWithSSL
    {
        [TestMethod]
        public void TestHandlingOfSSLInIE()
        {
            int counter = 0;
            ObjectRepository.Driver.Navigate().GoToUrl("https://cacert.org/");
            
            while(null != ObjectRepository.Driver.FindElements(By.Id("overridelink")).FirstOrDefault())
            {
                JavaScriptExecutor.ExecuteScript("javascript:document.getElementById('overridelink').click()");
                Thread.Sleep(3000);
                if (++counter == 5)
                    break;
            }
            Thread.Sleep(3000);
            
        }
    }
}
