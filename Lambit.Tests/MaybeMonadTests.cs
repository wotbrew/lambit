using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class MaybeMonadTests
    {

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

        [TestMethod]
        public void TestAggregateM()
        {
            var digits = new[] { "1", "2", "3" };
            var bad = new[]{"1", "poo", "3"};

            var six = digits.AggregateM(0, (st, x) => Parse.Int(x).Map(y => y + st));
            var nothing = bad.AggregateM(0, (st, x) => Parse.Int(x).Map(y => y + st));

            Assert.AreEqual(Maybe.Create(6), six);
            Assert.AreEqual(Maybe.Nothing, nothing);
        }

        [TestMethod]
        public void TestSumM()
        {
            var digits = new[] { "1", "2", "3" };
            var tens = new[] { "10", "10", "10" };
            var bad = new[] { "1", "333fsdfdsf", "10" };

            var six = digits.Select(Parse.Int).SumM(Monoid.IntAdd);
            var thousand = tens.Select(Parse.Int).SumM(Monoid.IntMult);
            var screwed = bad.Select(Parse.Int).SumM(Monoid.IntAdd);

            Assert.AreEqual(Maybe.Create(6), six);
            Assert.AreEqual(Maybe.Create(1000), thousand);
            Assert.AreEqual(Maybe.Nothing, screwed);
        }
    }
}
