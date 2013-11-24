using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class EitherFunctorTests
    {
        [TestMethod]
        public void TestMapBoth()
        {
            var either = new EitherBuilder<string, int>();

            var a = either.Left("foo");
            var b = either.Right(42);

            var foobar = a.MapBoth(x => x + "bar", y => y - 42);
            var zero = b.MapBoth(x => x + "bar", y => y - 42);

            Assert.AreEqual(either.Left("foobar"), foobar);
            Assert.AreEqual(either.Right(0), zero);
        }

        [TestMethod]
        public void TestMapLeft()
        {
            var either = new EitherBuilder<string, int>();

            var a = either.Left("foo");
            var b = either.Right(42);

            var foobar = a.MapLeft(x => x + "bar");
            var n42 = b.MapLeft(x => x + "bar");

            Assert.AreEqual(either.Left("foobar"), foobar);
            Assert.AreEqual(b, n42);
        }

        [TestMethod]
        public void TestMapRight()
        {
            var either = new EitherBuilder<string, int>();

            var a = either.Left("foo");
            var b = either.Right(42);

            var foo = a.MapRight(y => y - 42);
            var zero = b.MapRight(y => y - 42);

            Assert.AreEqual(either.Left("foo"), foo);
            Assert.AreEqual(either.Right(0), zero);
        }
    }
}
