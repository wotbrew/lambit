using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    /// <summary>
    /// Very simple immutable cons cell/singly linked list.
    /// O(1) addition O(n) iteration.
    /// </summary>
    public static class SinglyLinkedList
    {
        /// <summary>
        /// The empty list, useful sometimes as input to builder functions
        /// </summary>
        public struct Empty
        {
            /// <summary>
            /// Construct a new singly linked list
            /// </summary>
            public SinglyLinkedList<T> Add<T>(T value)
            {
                return new SinglyLinkedList<T>(value);
            }
        }

        /// <summary>
        /// Construct a new singly linked list
        /// </summary>
        public static SinglyLinkedList<T> Add<T>(T value)
        {
            return new SinglyLinkedList<T>(value);
        }
    }

    /// <summary>
    /// Very simple immutable cons cell/singly linked list.
    /// O(1) addition O(n) iteration.
    /// </summary>
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public T Value { get; private set; }
        public SinglyLinkedList<T> Rest { get; private set; }

        /// <summary>
        /// Construct a new singly linked list
        /// </summary>
        public SinglyLinkedList(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns a new SinglyLinkedList with the item added to the front.
        /// </summary>
        public SinglyLinkedList<T> Add(T value)
        {
            return new SinglyLinkedList<T>(value)
            {
                Rest = this
            };
        }

        /// <summary>
        /// Enumerate the list (from front to back)
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (var current = this; current != null; current = current.Rest)
                yield return current.Value;
        }

        /// <summary>
        /// Enumerate the list (from front to back)
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
