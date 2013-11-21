using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class LazyTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var lazy = Lazy.Create(() => "foo");
            Assert.AreEqual("foo", lazy.Value);
        }

        [TestMethod]
        public void TestMap()
        {
            var a = Lazy.Create(() => "foo");
            var b = a.Map((s) => s.ToUpper());
            Assert.AreEqual("FOO", b.Value);
        }

        [TestMethod]
        public void TestBind()
        {
            var a = Lazy.Create(() => "foo");
            var b = a.Bind((s) => Lazy.Create(() => s.ToUpper()));
            Assert.AreEqual("FOO", b.Value);
        }

        [TestMethod]
        public void TestLinq()
        {
            var lazy = from a in Lazy.Create(() => "foo")
                       let b = a + "bar"
                       from c in Lazy.Create(() => b.ToUpper())
                       select c;

            Assert.AreEqual("FOOBAR", lazy.Value);
        }

    }
}
