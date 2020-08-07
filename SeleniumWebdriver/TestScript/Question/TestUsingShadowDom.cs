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
    public class TestUsingShadowDom
    {

        private IWebElement GetWebElement(IWebElement webElement)
        {
            IWebElement element = (IWebElement)((IJavaScriptExecutor)ObjectRepository.Driver).ExecuteScript("return arguments[0].shadowRoot", webElement);
            return element;
        }

        [TestMethod]
        public void TestElementInShadowDom()
        {
            NavigationHelper.NavigateToUrl("https://shop.polymer-project.org/");
            var nodeOne = ObjectRepository.Driver.FindElement(By.CssSelector("shop-app"));
            var nodeOneExpand = GetWebElement(nodeOne);
            var nodeTwo = nodeOneExpand.FindElement(By.CssSelector("iron-pages"));
            //var nodeTwoExpand = GetWebElement(nodeTwo);
            var nodeThree = nodeTwo.FindElement(By.Name("home"));
            var nodeThreeExpand = GetWebElement(nodeThree);
            nodeThreeExpand.FindElement(By.CssSelector("shop-button")).Click();
            Assert.IsTrue(ObjectRepository.Driver.Url.Contains("mens_outerwear"),"Unable to click on the button");
            Thread.Sleep(3000);
        }
    }

}
