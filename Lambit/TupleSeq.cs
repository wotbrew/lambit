using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    /// <summary>
    /// A tuple seq is a singly linked list of tuples (pairs in this case)
    /// Items are added on to the front, its a quick and dirty way to build
    /// associations with full type inference.
    /// </summary>
    public static class TupleSeq
    {
        /// <summary>
        /// The empty list, useful sometimes as input to builder functions
        /// </summary>
        public struct Empty
        {
            /// <summary>
            /// Construct a new TupleSeq
            /// </summary>
            public TupleSeq<T, T2> Add<T, T2>(T a, T2 b)
            {
                return new TupleSeq<T, T2>(a, b);
            }
        }

        /// <summary>
        /// Construct a new TupleSeq
        /// </summary>
        public static TupleSeq<T, T2> Add<T, T2>(T a, T2 b)
        {
            return new TupleSeq<T, T2>(a, b);
        }
    }

    /// <summary>
    /// A tuple seq is a singly linked list of tuples (pairs in this case)
    /// Items are added on to the front, its a quick and dirty way to build
    /// associations with full type inference.
    /// </summary>
    public class TupleSeq<T, T2> : IEnumerable<Tuple<T, T2>>
    {
        public SinglyLinkedList<Tuple<T, T2>> List { get; private set; }
        public Tuple<T, T2> Value { get { return List.Value; } }
        public TupleSeq<T, T2> Rest { get { return List.Rest.WhenNotNull(x => new TupleSeq<T, T2>(x)); } }

        public T Item1 { get { return Value.Item1; } }
        public T2 Item2 { get { return Value.Item2; } }

        public TupleSeq(SinglyLinkedList<Tuple<T, T2>> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            List = list;
        }
        public TupleSeq(T a, T2 b)
            : this(new SinglyLinkedList<Tuple<T, T2>>(Tuple.Create(a, b)))
        {
        }

        /// <summary>
        /// Returns a new TupleSeq with the pair added to the front.
        /// </summary>
        public TupleSeq<T, T2> Add(T a, T2 b)
        {
            return new TupleSeq<T, T2>(List.Add(Tuple.Create(a, b)));
        }

        public IEnumerator<Tuple<T, T2>> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}
