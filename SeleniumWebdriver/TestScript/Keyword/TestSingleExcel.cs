using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumWebdriver.Keyword.DataEngineUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriver.TestScript.Keyword
{
    [TestClass]
    public class TestSingleExcel
    {
        [TestMethod, TestCategory("SingleTon")]
        public void TestReadWriteSingleExcel()
        {
            var keyword = new DataEngineUtility();
            keyword.ExecuteScript(@"C:\Data\log\KeywordTwo.xlsx", "TestSuite", "TC01");
        }

    }
}
