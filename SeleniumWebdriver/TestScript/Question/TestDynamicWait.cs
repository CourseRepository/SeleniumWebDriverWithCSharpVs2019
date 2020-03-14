using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
    public class TestDynamicWait
    {
        [TestMethod]
        public void TestDynamciWait()

        {

            NavigationHelper.NavigateToUrl("https://www.skyscanner.com.au/");

            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(50));//after 50sec its going to throw timeout exception

            wait.PollingInterval = TimeSpan.FromMilliseconds(250);//checks the wait logic for any exeptions

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));//if e find these exceptions then script doesnt stop it just igores those and continues

            // Waiting for the element to show up
            IWebElement inputBox = wait.Until(waitforSearchbox(By.Id("fsc-destination-search")));

            // Make sure the elemet is not null
            Assert.IsNotNull(inputBox, "Element not found 'fsc-destination-search'");

            inputBox.SendKeys("Osaka (Any)");

            //To dealy the script. To see the sends has put the value or not TODO : should be removed
            Thread.Sleep(3000);
            

        }

        private Func<IWebDriver, IWebElement> waitforSearchbox(By locator)
        {
            return ((x) =>
            {
                var elements = x.FindElements(locator);
                Console.WriteLine("Logging --->" + elements?.ToString());
                return elements.Count >= 1 ? elements.First() : null;
            });
        }

        
    }
}
