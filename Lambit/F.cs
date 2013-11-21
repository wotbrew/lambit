using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    /// <summary>
    /// Functional Prelude
    /// </summary>
    public static class F
    {
        /// <summary>
        /// Identity Function
        /// </summary>
        public static T Id<T>(T src)
        {
            return src;
        }

        /// <summary>
        /// Take the first element of a tuple
        /// </summary>
        public static T First<T, T2>(Tuple<T, T2> tuple)
        {
            if (tuple == null)
                throw new ArgumentNullException("tuple");

            return tuple.Item1;
        }

        /// <summary>
        /// Take the second element of a tuple
        /// </summary>
        public static T2 Second<T, T2>(Tuple<T, T2> tuple)
        {
            if (tuple == null)
                throw new ArgumentNullException("tuple");

            return tuple.Item2;
        }

        /// <summary>
        /// Take the first element of a key value pair
        /// </summary>
        public static T First<T, T2>(KeyValuePair<T, T2> pair)
        {
            return pair.Key;
        }

        /// <summary>
        /// Take the second element of a key value pair
        /// </summary>
        public static T2 Second<T, T2>(KeyValuePair<T, T2> pair)
        {
            return pair.Value;
        }

        /// <summary>
        /// If the item is null, apply f and return the result, else return the default value of T2.
        /// </summary>
        public static T2 WhenNotNull<T, T2>(this T src, Func<T, T2> f)
            where T : class
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return src != null ? f(src) : default(T2);
        }       

        /// <summary>
        /// If the item is null, apply f and return the result, else return the default value supplied.
        /// </summary>
        public static T2 WhenNotNull<T, T2>(this T src, Func<T, T2> f, T2 def)
            where T : class
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return src != null ? f(src) : def;
        }
    }
}
