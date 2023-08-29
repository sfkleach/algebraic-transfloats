using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransMaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransMaths.Tests
{
    [TestClass()]
    public class ITDoubleTests
    {
        [DataTestMethod()]
        [DataRow(0, 0, 0)]
        public void AddTest(double a, double b, double expected)
        {
            ITDouble actual = ITDouble.Make(a).Add(ITDouble.Make(b));
            Assert.IsTrue(actual.Equals(expected));
        }

        [TestMethod()]
        public void MulTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NegateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RecipocalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AntiRecipocalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SubTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DivideTest()
        {
            Assert.Fail();
        }
    }
}