using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.StepDefinition.Questions
{
    [Binding]
    public sealed class ElementInIframe
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private IWebDriver webDriver;
        private IWebElement webElement;
        private WebDriverWait wait;

        public ElementInIframe(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"I open the web page with url ""(.*)""")]
        public void GivenIOpenTheWebPageWithUrl(string url)
        {
            NavigationHelper.NavigateToUrl(url);
            webDriver = ObjectRepository.Driver;
        }

        [Given(@"I wait for page to loaded completely")]
        public void GivenIWaitForPageToLoadedCompletely()
        {
            
        }

        [Then(@"I switch to iframe with id ""(.*)""")]
        public void ThenISwitchToIframeWithId(string iframeId)
        {
            IWebElement frame = webDriver.FindElement(By.Id(iframeId));
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id(iframeId)));
           // webDriver.SwitchTo().Frame(frame);
        }

        [Then(@"Locate the element with id ""(.*)""")]
        public void ThenLocateTheElementWithId(string id)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(id)));
            webElement = webDriver.FindElement(By.Id(id));
        }

        [Then(@"Entre the information ""(.*)""")]
        public void ThenEntreTheInformation(string mail)
        {
            webElement.SendKeys(mail);
            webDriver.SwitchTo().DefaultContent();
            Thread.Sleep(3000);
        }

    }
}
