using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class EitherTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var str = new Either<int, string>("foo");
            var num = new Either<int, string>(42);

            Assert.AreEqual(Maybe.Nothing, str.Left);
            Assert.AreEqual(Maybe.Create("foo"), str.Right);

            Assert.AreEqual(Maybe.Create(42), num.Left);
            Assert.AreEqual(Maybe.Nothing, num.Right);
        }

        [TestMethod]
        public void TestResolve()
        {
            var dec = new Either<string, decimal>(42m);
            var str = new Either<string, int>("1");

            var stringy = dec.Resolve(Fun.Id, Fun.ToString);
            var inty = str.Resolve(int.Parse, Fun.Id);

            Assert.AreEqual("42", stringy);
            Assert.AreEqual(1, inty);
        }

        [TestMethod]
        public void TestJoin()
        {
            var a = Either.Left<string, string>("foo");
            Assert.AreEqual("foo", a.Join());
        }
    }
}
