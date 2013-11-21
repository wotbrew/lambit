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
        public void TestMap()
        {
            var justfoo = Maybe.Create("foo");
            var nothing = default(Maybe<string>);

            var upper = justfoo.Map(x => x.ToUpper());
            var upperNothing = nothing.Map(x => x.ToUpper());

            Assert.AreEqual("FOO", upper.OrDefault());
            Assert.AreEqual(Maybe.Nothing, upperNothing);
        }

        [TestMethod]
        public void TestBind()
        {
            var justfoo = Maybe.Create("foo");
            var nothing = default(Maybe<string>);

            var upper = justfoo.Bind(x => Maybe.Create(x.ToUpper()));
            var foosGone = justfoo.Bind(x => default(Maybe<int>));
            var upperNothing = nothing.Bind(x => Maybe.Create(x.ToUpper()));

            Assert.AreEqual("FOO", upper.OrDefault());
            Assert.AreEqual(Maybe.Nothing, foosGone);
            Assert.AreEqual(Maybe.Nothing, upperNothing);
        }

        [TestMethod]
        public void TestLinq()
        {
            var just = from x in Maybe.Create(42)
                       from y in Maybe.Create("foo")
                       select y + x;
            var none = from x in Maybe.Create(42)
                       from y in default(Maybe<string>)
                       select y + x;

            Assert.AreEqual("foo42", just.OrDefault());
            Assert.AreEqual(Maybe.Nothing, none);

        }
    }
}
