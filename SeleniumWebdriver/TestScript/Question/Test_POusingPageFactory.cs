using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;


namespace SeleniumWebdriver.TestScript.Question
{
    [TestClass]
    public class Test_POusingPageFactory
    {
        [TestMethod]
        public void TestPage()
        {
            NavigationHelper.NavigateToUrl("https://www.skyscanner.com/inspire/map?outboundDate=2019-12-17&outboundPlace=SJC&preferDirects=false");
            IWebElement webElement =  ObjectRepository.Driver.FindElement(By.CssSelector("div#searchpanel > .elevated.search-bar .input-wrapper .origin-input.tt-input"));
            webElement.Clear();
            webElement.SendKeys("Testing");
        }
    }
}
