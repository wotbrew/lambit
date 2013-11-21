using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lambit.Tests
{
    [TestClass]
    public class SinglyLinkedListTests
    {
        [TestMethod]
        public void TestAddFromEmpty()
        {
            var empty = new SinglyLinkedList.Empty();
            var list = empty.Add("foo");

            Assert.AreEqual("foo", list.Value);
            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void TestAddStatic()
        {
            var list = SinglyLinkedList.Add("foo");

            Assert.AreEqual("foo", list.Value);
            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void TestAdd()
        {
            var list = SinglyLinkedList
                .Add("foo")
                .Add("bar")
                .Add("fred");

            Assert.AreEqual("fred", list.Value);
            Assert.IsNotNull(list.Rest);
            Assert.AreEqual("bar", list.Rest.Value);
            Assert.IsNotNull(list.Rest.Rest);
            Assert.AreEqual("foo", list.Rest.Rest.Value);
            Assert.IsNull(list.Rest.Rest.Rest);
        }

        [TestMethod]
        public void TestEnumeration()
        {
            var list = SinglyLinkedList
                .Add("foo")
                .Add("bar")
                .Add("fred");
            Assert.IsTrue(list.SequenceEqual(new[] { "fred", "bar", "foo" }));
        }
    }
}
