using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.StepDefinition
{
    [Binding]
    public sealed class FeatureContextStepDfn
    {
        private readonly ScenarioContext context;
        private readonly FeatureContext featureContext;

        public FeatureContextStepDfn(FeatureContext injectedFeatureContext, ScenarioContext injectedContext)
        {
            context = injectedContext;
            featureContext = injectedFeatureContext;
        }

        [Given(@"I have the feature context injected in the constructor")]
        public void GivenIHaveTheFeatureContextInjectedInTheConstructor()
        {
            Console.WriteLine(featureContext.FeatureInfo.Title);
        }

        [Given(@"I get the name of the feature getting executed")]
        public void GivenIGetTheNameOfTheFeatureGettingExecuted()
        {
            Console.WriteLine(featureContext.FeatureInfo.Description);
        }
    }
}
