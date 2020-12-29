using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Configuration;
using SeleniumWebdriver.CustomException;
using SeleniumWebdriver.Reports;
using SeleniumWebdriver.Settings;
using TechTalk.SpecFlow;

namespace SeleniumWebdriver.BaseClasses
{
    [TestClass]
    
    public class BaseClass 
    {
        private static readonly ILog Logger = Log4NetHelper.GetXmlLogger(typeof (BaseClass));
        private static FirefoxProfile GetFirefoxProfile()
        {
            FirefoxProfile profile = new FirefoxProfile();
            FirefoxProfileManager manager = new FirefoxProfileManager();
            //profile = manager.GetProfile("default");
            Logger.Info(" Using Firefox Profile ");
            return profile;
        }

        private static FirefoxOptions GetFirefoxOptions()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.Profile = GetFirefoxProfile();
            firefoxOptions.AcceptInsecureCertificates = true;
            return firefoxOptions;    
        }

        private static FirefoxOptions GetOptions()
        {
            FirefoxProfileManager manager = new FirefoxProfileManager();

            FirefoxOptions options = new FirefoxOptions()
            {
                Profile = manager.GetProfile("default"),
                AcceptInsecureCertificates = true,

            };
            return options;
        }
        private static ChromeOptions GetChromeOptions()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("start-maximized");
            option.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true, true);
            //option.AddArgument("--headless");
            //option.AddExtension(@"C:\Users\rahul.rathore\Desktop\Cucumber\extension_3_0_12.crx");
            Logger.Info(" Using Chrome Options ");
            return option;
        }

        private static InternetExplorerOptions GetIEOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                //EnsureCleanSession = true,
                ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom,
                //AcceptInsecureCertificates = true,
                IgnoreZoomLevel = true,
                PageLoadStrategy = PageLoadStrategy.Normal,
                UnhandledPromptBehavior =  UnhandledPromptBehavior.Dismiss,
                EnableNativeEvents = true,
            };
            options.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
            options.AddAdditionalCapability(CapabilityType.AcceptInsecureCertificates, true);
            Logger.Info(" Using Internet Explorer Options ");
            return options;
        }

        
        private static FirefoxDriver GetFirefoxDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            FirefoxDriver driver = new FirefoxDriver(GetFirefoxOptions());
            return driver;
        }

        private static ChromeDriver GetChromeDriver()
        {
            ChromeDriver driver = new ChromeDriver(GetChromeOptions());
            return driver;
        }

        private static InternetExplorerDriver GetIEDriver()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver(GetIEOptions());
            return driver;
        }

        private static EdgeDriver GetEdgeDriver()
        {
            EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService();
            edgeDriverService.HideCommandPromptWindow = true;
            EdgeDriver edgeDriver = new EdgeDriver(edgeDriverService);
            return edgeDriver;
        }
        /*================== Support for PhantomJs has been stoped in recent version of Selenium ==============
         
            Use Chrome or firefox headless mode
             
             */
        //private static PhantomJSDriver GetPhantomJsDriver()
        //{
        //    PhantomJSDriver driver = new PhantomJSDriver(GetPhantomJsDrvierService());

        //    return driver;
        //}

        //private static PhantomJSOptions GetPhantomJsptions()
        //{
        //    PhantomJSOptions option = new PhantomJSOptions();
        //    option.AddAdditionalCapability("handlesAlerts", true);
        //    Logger.Info(" Using PhantomJS Options  ");
        //    return option;
        //}

        //private static PhantomJSDriverService GetPhantomJsDrvierService()
        //{
        //    PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
        //    service.LogFile = "TestPhantomJS.log";
        //    service.HideCommandPromptWindow = false;
        //    service.LoadImages = true;
        //    Logger.Info(" Using PhantomJS Driver Service  ");
        //    return service;
        //}


        [AssemblyInitialize]
        //[BeforeFeature()]
        public static void InitWebdriver(TestContext tc)
        {
            ObjectRepository.Config = new AppConfigReader();
            Reporter.GetReportManager();
            Reporter.AddTestCaseMetadataToHtmlReport(tc);
            switch (ObjectRepository.Config.GetBrowser())
            {
                case BrowserType.Firefox:
                    ObjectRepository.Driver = GetFirefoxDriver();
                    Logger.Info(" Using Firefox Driver  ");

                    break;

                case BrowserType.Chrome:
                    ObjectRepository.Driver = GetChromeDriver();
                    Logger.Info(" Using Chrome Driver  ");
                    break;

                case BrowserType.IExplorer:
                    ObjectRepository.Driver = GetIEDriver();
                    Logger.Info(" Using Internet Explorer Driver  ");
                    break;

                // Deprecated 
                case BrowserType.PhantomJs:
                    //ObjectRepository.Driver = GetPhantomJsDriver();
                    Logger.Info(" Using PhantomJs Driver  ");
                    break;

                case BrowserType.Edge:
                    ObjectRepository.Driver = GetEdgeDriver();
                    Logger.Info(" Using Edge Driver  ");
                    break;

                default:
                    throw new NoSutiableDriverFound("Driver Not Found : " + ObjectRepository.Config.GetBrowser().ToString()); 
            }
            ObjectRepository.Driver.Manage().Cookies.DeleteAllCookies();
            ObjectRepository.Driver.Manage()
                .Timeouts().PageLoad = TimeSpan.FromSeconds(ObjectRepository.Config.GetPageLoadTimeOut());
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut());
            ObjectRepository.Driver.Manage().Cookies.DeleteAllCookies();
            BrowserHelper.BrowserMaximize();
        }


        [AssemblyCleanup]
        //[AfterScenario()]
        public static void TearDown()
        {
            if (ObjectRepository.Driver != null)
            {
               ObjectRepository.Driver.Close();
               ObjectRepository.Driver.Quit();
            }
            Logger.Info(" Stopping the Driver  ");
        }
    }
}
