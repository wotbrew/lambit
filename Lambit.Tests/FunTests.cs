using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class FunTests
    {
        [TestMethod]
        public void TestId()
        {
            var x = 1;
            Assert.AreEqual(1, Fun.Id(x));
        }

        [TestMethod]
        public void TestFirst()
        {
            var pair = Tuple.Create("foo", "bar");
            var pair2 = new KeyValuePair<string, string>("fred", "jim");

            var a = Fun.First(pair);
            var b = Fun.First(pair2);

            Assert.AreEqual("foo", a);
            Assert.AreEqual("fred", b);
        }

        [TestMethod]
        public void TestSecond()
        {

            var pair = Tuple.Create("foo", "bar");
            var pair2 = new KeyValuePair<string, string>("fred", "jim");

            var a = Fun.Second(pair);
            var b = Fun.Second(pair2);

            Assert.AreEqual("bar", a);
            Assert.AreEqual("jim", b);
        }
    }
}
