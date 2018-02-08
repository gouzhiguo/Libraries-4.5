using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EC.Libraries.UnitTest
{
    [TestClass]
    public class LingTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((dd, aa) => dd.Length < aa);


            var ss = shortDigits;

 

        }
    }
}
