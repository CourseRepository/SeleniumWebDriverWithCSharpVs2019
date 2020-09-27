using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumWebdriver.ComponentHelper;
using SeleniumWebdriver.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriver.TestScript.Question
{
    [TestClass]
    public class TestIsTextPresent
    {
        [TestMethod]
        public void TestTextPresent()
        {
            NavigationHelper.NavigateToUrl("https://www.w3schools.com/");
            Assert.IsTrue(GenericHelper.IsTextPresent(ObjectRepository.Driver, "Learn Colors"));

        }
    }
}
