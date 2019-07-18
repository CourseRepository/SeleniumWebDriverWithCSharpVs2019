using SeleniumWebdriver.ComponentHelper;
using System;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.GeneralHook
{
    [Binding]
    public sealed class GeneralHook
    {
        private static ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;

        
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("BeforeTestRun Hook");
            
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("AfterTestRun Hook");
        }

        [BeforeFeature("Tag1")]
        public static void BeforeFeature()
        {
            Console.WriteLine("BeforeFeature Hook");
        }

        [AfterFeature("Tag1")]
        public static void AfterFeature()
        {
            Console.WriteLine("AfterFeature Hook");
        }

        [BeforeScenario("Tag1")]
        public static void BeforeScenario()
        {
            Console.WriteLine("BeforeScenario Hook");
        }

        [BeforeScenario]
        public static void BeforeScenarioContextInjection(FeatureContext featureContext,ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Console.WriteLine("AfterScenario Hook");
            if (_scenarioContext.TestError != null)
            {
                string name = _scenarioContext.ScenarioInfo.Title + ".jpeg";
                GenericHelper.TakeScreenShot(name);
                Console.WriteLine(_scenarioContext.TestError.Message);
                Console.WriteLine(_scenarioContext.TestError.StackTrace);
            }
        }
    }
}
