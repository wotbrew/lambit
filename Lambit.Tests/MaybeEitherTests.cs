using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class MaybeEitherTests
    {
        [TestMethod]
        public void TestOrValue()
        {
            var good = new
            {
                One = "1",
                Two = "2",
                Pi = "3.14",
            };
            var proc = new
            {
                One = 1,
                Two = 2,
                Pi = 3.14
            };

            var parsey = Fun.FnOver(good, data =>
                         from one in Parse.Int(data.One).OrValue("invalid 'one'")
                         from two in Parse.Int(data.Two).OrValue("invalid 'two'")
                         from pi in Parse.Double(data.Pi).OrValue("invalid 'pi'")
                         select new
                         {
                             One = one,
                             Two = two,
                             Pi = pi
                         });

            var parsedGood = parsey(good);
            var parsedBad = parsey(new
            {
                One = "1",
                Two = "ff2",
                Pi = "3.14",
            });

            var either = EitherBuilder.Of(proc, "");

            Assert.AreEqual(either.Left(proc), parsedGood);
            Assert.AreEqual(either.Right("invalid 'two'"), parsedBad);
        }

        [TestMethod]
        public void TestResolve()
        {
            var good = new
            {
                One = "1",
                Two = "2",
                Pi = "3.14",
            };
            var proc = new
            {
                One = 1,
                Two = 2,
                Pi = 3.14
            };

            var parsey = Fun.FnOver(good, data =>
                         from one in Parse.Int(data.One).Resolve(() => "invalid 'one'")
                         from two in Parse.Int(data.Two).Resolve(() => "invalid 'two'")
                         from pi in Parse.Double(data.Pi).Resolve(() => "invalid 'pi'")
                         select new
                         {
                             One = one,
                             Two = two,
                             Pi = pi
                         });

            var parsedGood = parsey(good);
            var parsedBad = parsey(new
            {
                One = "1",
                Two = "ff2",
                Pi = "3.14",
            });

            var either = EitherBuilder.Of(proc, "");

            Assert.AreEqual(either.Left(proc), parsedGood);
            Assert.AreEqual(either.Right("invalid 'two'"), parsedBad);
        }
    }
}
