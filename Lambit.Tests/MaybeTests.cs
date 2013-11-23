using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class MaybeTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var b = Maybe.Create("foo");
            var m = Maybe.Create("bar");
            var n = new Maybe<string>();

            Assert.IsTrue(b.HasValue);
            Assert.IsTrue(m.HasValue);
            Assert.IsFalse(n.HasValue);
        }

        [TestMethod]
        public void TestResolve()
        {
            var a = Maybe.Create("foo");
            var b = new Maybe<string>();

            Assert.AreEqual("foo", a.Resolve(() => "bla"));
            Assert.AreEqual("bla", b.Resolve(() => "bla"));
        }

        [TestMethod]
        public void TestOrDefault()
        {
            var a = Maybe.Create("foo");
            var b = new Maybe<string>();

            Assert.AreEqual("foo", a.OrDefault());
            Assert.IsNull(b.OrDefault());
        }

        [TestMethod]
        public void TestEquality()
        {
            var n = new Nothing();
            Maybe<int> x = Maybe.Nothing;
            var y = new Maybe<int>();
            var z = default(Maybe<int>);

            var a = Maybe.Create(42);

            Assert.AreEqual(n, x);
            Assert.AreEqual(n, y);
            Assert.AreEqual(n, z);
            Assert.AreNotEqual(n, a);

            Assert.AreEqual(x, y);
            Assert.AreEqual(x, z);
            Assert.AreNotEqual(x, a);

            Assert.AreEqual(y, z);
            Assert.AreNotEqual(y, a);
        }


        [TestMethod]
        public void TestFlatten()
        {
            var m = Maybe.Create(Maybe.Create(1));
            var flat = m.Flatten();

            Assert.AreEqual(Maybe.Create(1), flat);
        }

        [TestMethod]
        public void TestAlt()
        {
            var a = Maybe.Create(1);
            var b = default(Maybe<string>);

            var alta = a.Alt(() => Maybe.Create(2));
            var altb = b.Alt(() => Maybe.Create("foo"));

            Assert.AreEqual(Maybe.Create(1), alta);
            Assert.AreEqual(Maybe.Create("foo"), altb);
        }

        [TestMethod]
        public void TestAs()
        {
            IMaybe a = new Maybe<int>(1);
            IMaybe b = Maybe.Nothing;

            Assert.AreEqual(Maybe.Create(1), a.As<int>());
            Assert.AreEqual(Maybe.Nothing, a.As<string>());
        }

        [TestMethod]
        public void TestWhere()
        {
            var a = Maybe.Create(1);
            var b = Maybe.Create(2);
            var c = Maybe.Create(4);

            Assert.AreEqual(Maybe.Nothing, a.Where(x => x % 2 == 0));
            Assert.AreEqual(b, b.Where(x => x % 2 == 0));
            Assert.AreEqual(c, c.Where(x => x % 2 == 0));
        }

    }
}
