using OpenQA.Selenium;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.StepDefinition
{


    //https://docs.specflow.org/projects/specflow/en/latest/Integrations/MsTest.html#deploymentitem

    //https://github.com/SpecFlowOSS/SpecFlow/issues/803


    [Binding]
    public partial class FileUploadStepDfn
    {
        [Given(@"I nagivate to the bugzila web page")]
        public void GivenINagivateToTheBugzilaWebPage()
        {
            NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetWebsite());
        }

        [Given(@"I login in the bugzila application")]
        public void GivenILoginInTheBugzilaApplication()
        {
            ButtonHelper.ClickButton(By.Id("enter_bug"));
            TextBoxHelper.TypeInTextBox(By.Id("Bugzilla_login"), ObjectRepository.Config.GetUsername());
            TextBoxHelper.TypeInTextBox(By.Id("Bugzilla_password"), ObjectRepository.Config.GetPassword());
            ButtonHelper.ClickButton(By.Id("log_in"));
        }

        [Then(@"I click on ""(.*)"" link")]
        public void ThenIClickOnLink(string linkText)
        {
            ButtonHelper.ClickButton(By.LinkText(linkText));
        }

        [Then(@"I click on the add attachment button and upload the file")]
        public void ThenIClickOnTheAddAttachmentButtonAndUploadTheFile()
        {
            ButtonHelper.ClickButton(By.XPath("//div[@id='attachment_false']/input"));
            GenericHelper.WaitForWebElement(By.Id("data"), TimeSpan.FromSeconds(30));
            JavaScriptExecutor.ScrollIntoViewAndClick(By.Id("data"));
            //ButtonHelper.ClickButton(By.Id("data"));

            var processinfo = new ProcessStartInfo()
            {
                FileName = "\"" + Directory.GetCurrentDirectory() + @"\Resources\FileUpload.exe" + "\"",
                Arguments = "\"" + Directory.GetCurrentDirectory() + @"\Resources\ExcelData.xlsx" + "\"",
                UseShellExecute = false
            };
            using (var process = Process.Start(processinfo))
            {
                process.WaitForExit();
            }
            Thread.Sleep(5000);
        }

        [Then(@"I logout from the application")]
        public void ThenILogoutFromTheApplication()
        {
            ButtonHelper.Logout();
        }


    }
}
