using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.StepDefinition
{
    [Binding]
    public partial class FileUploadStepDfn
    {
        [Given(@"I nagivate to google webpage")]
        public void GivenINagivateToGoogleWebpage()
        {
            Console.WriteLine("I am at the google page");
        }

        [Given(@"I deploy the item present in Resource folder")]
        public void GivenIDeployTheItemPresentInResourceFolder()
        {
            Console.WriteLine("Deploying the item");
        }

        [Then(@"I verify that the file is present in required direcotry")]
        public void ThenIVerifyThatTheFileIsPresentInRequiredDirecotry()
        {
            Console.WriteLine("Item deploy");
        }

    }
}
