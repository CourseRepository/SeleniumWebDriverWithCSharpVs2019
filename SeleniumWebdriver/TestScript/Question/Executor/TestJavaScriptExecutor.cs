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

namespace SeleniumWebdriver.TestScript.Question.Executor
{
    [TestClass]
    public class TestJavaScriptExecutor
    {

        private readonly By CourseLink = By.XPath("//div[@data-purpose='course-container']/a[@href='/course/selenium-webdriver-page-objects/']");
        private readonly By ShowMore = By.XPath("//h2[@data-purpose='title']/following-sibling::div/button");

        [TestMethod]
        public void TestScrollClick()
        {
            NavigationHelper.NavigateToUrl("https://www.udemy.com/course/bdd-with-selenium-webdriver-and-speckflow-using-c/");

            IJavaScriptExecutor executor = (IJavaScriptExecutor)ObjectRepository.Driver;

            IWebElement element = ObjectRepository.Driver.FindElement(ShowMore);

            executor.ExecuteScript("window.scrollTo(0," + (element.Location.Y - 100) + ")");

            element.Click();

            element = ObjectRepository.Driver.FindElement(CourseLink);

            executor.ExecuteScript("window.scrollTo(0," + (element.Location.Y - 100) + ")");

            element.Click();

            Thread.Sleep(200);
        }
    }
}
