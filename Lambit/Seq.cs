using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class Seq
    {
        /// <summary>
        /// Take the sequence, or in the case of null, return an empty sequence of the same type.
        /// </summary>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> src)
        {
            return src ?? new T[] { };
        }

        /// <summary>
        /// Take all values in the sequence where the Maybe contains a value.
        /// </summary>
        public static IEnumerable<T> Choose<T>(this IEnumerable<Maybe<T>> src)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            return src
                .Where(x => x.HasValue)
                .Select(x => x.OrDefault());
        }

        /// <summary>
        /// Take all values in the sequence where the function f returns a value.
        /// </summary>
        public static IEnumerable<T2> Choose<T, T2>(this IEnumerable<T> src,
            Func<T, Maybe<T2>> f)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            if(f == null)
                throw new ArgumentNullException("f");

            return src
                .Select(f)
                .Choose();
        }

        /// <summary>
        /// Take a slice of the list between the start (inclusive) and end (exclusive) index.
        /// </summary>
        public static IEnumerable<T> Slice<T>(this IList<T> src, int startInc, int endExc)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            for (int i = startInc; i < endExc && i < src.Count; i++)
            {
                yield return src[i];
            }
        }

        /// <summary>
        /// Take a slice of the list from the start index (inclusive).
        /// </summary>
        public static IEnumerable<T> Slice<T>(this IList<T> src, int startInc)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            return src.Slice(startInc, src.Count);
        }

        /// <summary>
        /// Take a sum of the elements in the sequence using the given monoid instance.
        /// </summary>
        public static T Sum<T>(this IEnumerable<T> src, IMonoid<T> monoid)
        {
            if (monoid == null)
                throw new ArgumentNullException("monoid");

            return src.Aggregate(monoid.Identity, monoid.Concat);
        }
    }
}
