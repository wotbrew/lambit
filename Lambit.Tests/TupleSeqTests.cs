using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class TupleSeqTests
    {
        [TestMethod]
        public void TestAddFromEmpty()
        {
            var empty = new TupleSeq.Empty();
            var list = empty.Add("foo", 1);

            Assert.AreEqual(Tuple.Create("foo", 1), list.Value);
            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void TestAddStatic()
        {
            var list = TupleSeq.Add("foo", 1);

            Assert.AreEqual(Tuple.Create("foo", 1), list.Value);
            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void TestAdd()
        {
            var list = TupleSeq
                .Add("foo", 1)
                .Add("bar", 2)
                .Add("fred", 3);

            Assert.AreEqual(Tuple.Create("fred", 3), list.Value);
            Assert.IsNotNull(list.Rest);
            Assert.AreEqual(Tuple.Create("bar", 2), list.Rest.Value);
            Assert.IsNotNull(list.Rest.Rest);
            Assert.AreEqual(Tuple.Create("foo", 1), list.Rest.Rest.Value);
            Assert.IsNull(list.Rest.Rest.Rest);
        }

        [TestMethod]
        public void TestEnumeration()
        {
            var list = TupleSeq
                .Add("foo", 1)
                .Add("bar", 2)
                .Add("fred", 3);
            Assert.IsTrue(list.SequenceEqual(new[] { Tuple.Create("fred", 3), Tuple.Create("bar", 2), Tuple.Create("foo", 1) }));
        }
    }
}
