using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class MaybeFunctorTests
    {

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
    }
}
