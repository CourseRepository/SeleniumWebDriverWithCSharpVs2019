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

namespace SeleniumWebdriver.TestScript.Question.Wait
{
	[TestClass]
	public class DynamicWait
	{
		[TestMethod]
		public void TestDynamicWait()
		{
			NavigationHelper.NavigateToUrl("https://www.udemy.com/");
			ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
			WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(50));
			wait.PollingInterval = TimeSpan.FromMilliseconds(250);
			wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
			wait.Until(WaitForElement()).SendKeys("Health");
			ButtonHelper.ClickButton(By.CssSelector(".notice-streamer .input-group-btn .btn-link"));
			//wait.Until(WaitForLastElement()).Click();
			//Console.WriteLine(wait.Until(WaitForPageTitle()));

		}

		private Func<IWebDriver, IWebElement> WaitForElement()

		{
			return ((x) =>
			{
				Console.WriteLine("Waiting for element");
				if (x.FindElements(By.Id("header-desktop-search-bar")).Count == 1)
					return x.FindElement(By.Id("header-desktop-search-bar"));
				return null;
			});

		}
	}
}
