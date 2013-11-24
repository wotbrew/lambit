using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class EitherMonadTests
    {
        [TestMethod]
        public void TestLINQ()
        {
            var either = new EitherBuilder<int, string>();

            var n42 = from hundred in either.Left(100)
                      from fifty in either.Left(50)
                      select hundred - fifty - 8;

            var error = from hundred in either.Left(100)
                        from fifty in either.Right("error!")
                        select hundred - fifty - 8;

            Assert.AreEqual(either.Left(42), n42);
            Assert.AreEqual(either.Right("error!"), error);
        }
    }
}
