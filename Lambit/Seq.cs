using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class Seq
    {
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> src)
        {
            return src ?? new T[] { };
        }

        public static IEnumerable<T> Choose<T>(this IEnumerable<Maybe<T>> src)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            return src
                .Where(x => x.HasValue)
                .Select(x => x.OrDefault());
        }

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

        public static IEnumerable<T> Slice<T>(this IList<T> src, int startInc, int endExc)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            for (int i = startInc; i < endExc && i < src.Count; i++)
            {
                yield return src[i];
            }
        }

        public static IEnumerable<T> Slice<T>(this IList<T> src, int startInc)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            return src.Slice(startInc, src.Count);
        }
    }
}
