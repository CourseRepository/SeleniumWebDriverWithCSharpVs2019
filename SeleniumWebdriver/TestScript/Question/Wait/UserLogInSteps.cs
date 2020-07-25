using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumWebdriver.TestScript.Question.Wait
{
    public class UserLogInSteps
    {
        public IWebDriver driver;
        private WebDriverWait wait;
        
        [Given(@"I have navigated to my test site")]
        public void GivenIHaveNavigatedToMyTestSite()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver driver = new ChromeDriver();
            driver.Url = "";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500),
            };
        }

        [Given(@"I enter my login information")]
        public void GivenIEnterMyLoginInformation()
        {
            //Thread.Sleep(2000);
            IWebElement webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"email\"]")));
            webElement.SendKeys("xxx");
        }

        [When(@"I click the Sign In button")]
        public void WhenIClickTheSignInButton()
        {
            //Thread.Sleep(2000);
            IWebElement webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"SubmitLogin\"]/span ")));
            webElement.Click();
        }

        [Then(@"I successfully login to the site")]
        public void ThenISuccessfullyLoginToTheSite()
        {
            //Thread.Sleep(2000);
            Assert.IsTrue(wait.Until(waitForTextInPageSource("My account")));
            //Assert.IsTrue(driver.PageSource.Contains("My account"));

        }

        private Func<IWebDriver, bool> waitForTextInPageSource(string value)
        {
            return (driver) =>
            {
                return driver.PageSource.Contains(value);
            };
        }

        private int getRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 4);
        }
    }
}
