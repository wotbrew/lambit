using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class FTests
    {
        [TestMethod]
        public void TestId()
        {
            var x = 1;
            Assert.AreEqual(1, F.Id(x));
        }

        [TestMethod]
        public void TestFirst()
        {
            var pair = Tuple.Create("foo", "bar");
            var pair2 = new KeyValuePair<string, string>("fred", "jim");

            var a = F.First(pair);
            var b = F.First(pair2);

            Assert.AreEqual("foo", a);
            Assert.AreEqual("fred", b);
        }

        [TestMethod]
        public void TestSecond()
        {

            var pair = Tuple.Create("foo", "bar");
            var pair2 = new KeyValuePair<string, string>("fred", "jim");

            var a = F.Second(pair);
            var b = F.Second(pair2);

            Assert.AreEqual("bar", a);
            Assert.AreEqual("jim", b);
        }
    }
}
