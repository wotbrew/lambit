using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void TestMatch()
        {
            var a = new[]{1,2,3};
            var b = new int[]{};
            string[] d = null;
            
            var six = a.Match()
                .Case((x, y) => x - y)
                .CaseCons((x, y, z, g, vs) => 42)
                .Case((x, y, z) => x + y + z)
                .CaseElse((n) => 0);

            var empty = b.Match()
                .CaseNull(() => "null")
                .CaseEmpty(() => "empty")
                .CaseElse((n) => "i don't know");

            var nil = d.Match()
                .CaseNull(() => "null")
                .CaseEmpty(() => "empty")
                .CaseElse((n) => "i don't know");

            Assert.AreEqual(6, six);
            Assert.AreEqual("empty", empty);
            Assert.AreEqual("null", nil);

        }
    }
}
