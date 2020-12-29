using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
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
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentHtmlReporter = new ExtentHtmlReporter(@"C:\Data\log\");
            _extentHtmlReporter.Config.ReportName = "testreport.html";
            _extentHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if(null != featureContext)
            {
                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if(null != scenarioContext)
            {
                _scenarioContext = scenarioContext;
                _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            }
        }

        [AfterStep]
        public void AfterEachStep()
        {
            // logic -> Given ,when or then
            // add the node

            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;

            //switch case

            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    /*  if (_scenarioContext.TestError != null)
                      {
                          _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                      }
                      else
                      {
                          _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                      }*/
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    /*   if (_scenarioContext.TestError != null)
                       {
                           _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                       }
                       else
                       {
                           _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                       }
   */
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    /* if (_scenarioContext.TestError != null)
                     {
                         _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message+ "\n" + _scenarioContext.TestError.StackTrace);
                     }
                     else
                     {
                         _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                     }
 */
                    CreateNode<Then>();
                    break;
                default:
                    /*if (_scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                    }
                    else
                    {
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                    }*/
                    CreateNode<And>();

                    break;
            }

        }

        public void CreateNode<T>() where T : IGherkinFormatterModel
        {
            if (_scenarioContext.TestError != null)
            {
                string name = @"C:\Data\log\" + _scenarioContext.ScenarioInfo.Title.Replace(" ", "") + ".jpeg";
                GenericHelper.TakeScreenShot(name);
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace)
                    .AddScreenCaptureFromPath(name);
            }
            else
            {
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }
             
        [BeforeFeature("Tag1")]
        public static void BeforeFeature(FeatureContext featureContext)
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

        [BeforeScenario("Tag2")]
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
                string name = _scenarioContext.ScenarioInfo.Title.Replace(" ","") + ".jpeg";
                GenericHelper.TakeScreenShot(name);
                //GenericHelper.TakeScreenShot("testdir",name);
                Console.WriteLine(_scenarioContext.TestError.Message);
                Console.WriteLine(_scenarioContext.TestError.StackTrace);
            }
        }
    }
}
