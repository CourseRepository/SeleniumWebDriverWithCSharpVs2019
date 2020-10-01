using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.StepDefinition.Questions
{
    [Binding]
    public sealed class FlieUpload
    {
        [Given(@"I navigate to the bug zila application ""(.*)""")]
        public void GivenINavigateToTheBugZilaApplication(string url)
        {
            NavigationHelper.NavigateToUrl(url);
        }

        [Given(@"I click on the element with locator ""(.*)""")]
        public void GivenIClickOnTheElementWithLocator(string id)
        {
            ButtonHelper.ClickButton(By.Id(id));
        }

        [Given(@"I login into the application with  usernam ""(.*)"" and password ""(.*)""")]
        public void GivenILoginIntoTheApplicationWithUsernamAndPassword(string user, string pass)
        {
            TextBoxHelper.TypeInTextBox(By.Id("Bugzilla_login"), user);
            TextBoxHelper.TypeInTextBox(By.Id("Bugzilla_password"), pass);
        }

        [Given(@"I press on Login button")]
        public void GivenIPressOnLoginButton()
        {
            ButtonHelper.ClickButton(By.Id("log_in"));
        }

        [Given(@"I click on ""(.*)"" link")]
        public void GivenIClickOnLink(string link)
        {
            LinkHelper.ClickLink(By.LinkText(link));
        }

        [Then(@"I click on button ""(.*)""")]
        public void ThenIClickOnButton(string btn)
        {
            ButtonHelper.ClickButton(By.Id(btn));
        }

        [Then(@"Wait for the attachment button ""(.*)"" for ""(.*)"" seconds")]
        public void ThenWaitForTheAttachmentButtonForSeconds(string btnId, int timeOut)
        {
            GenericHelper.WaitForWebElement(By.Id(btnId), TimeSpan.FromSeconds(timeOut));
            JavaScriptExecutor.ScrollIntoViewAndClick(By.Id(btnId));
        }


        [Then(@"I upload the file ""(.*)"" present in Resources")]
        public void ThenIUploadTheFilePresentInResources(string fileName)
        {
            var processinfo = new ProcessStartInfo()
            {
                FileName = "\"" + Directory.GetCurrentDirectory() + @"\Resources\FileUpload.exe" + "\"",
                Arguments = "\"" + Directory.GetCurrentDirectory() + @"\Resources\" + fileName + "\"",
                UseShellExecute = false
            };
            using (var process = Process.Start(processinfo))
            {
                process.WaitForExit();
            }
            Thread.Sleep(3000); // Just to see in UI
        }

        [Then(@"logout from the application")]
        public void ThenLogoutFromTheApplication()
        {
            ButtonHelper.Logout();
        }


    }
}
