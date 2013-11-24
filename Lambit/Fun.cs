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
    public static class Fun
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

        /// <summary>
        /// Apply the function f to the value, allows left to right 'pipe' style composition.
        /// e.g f(g(h)) == h.Apply(g).Apply(f)
        /// </summary>
        public static T2 Apply<T, T2>(this T value, Func<T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return f(value);
        }

        /// <summary>
        /// Simply calls ToString on the given value.
        /// </summary>
        public static string ToString<T>(T value)
        {
            return value.ToString();
        }
        /// <summary>
        /// Simply calls ToString on the given reference-type value.
        /// If the value is null, return null rather than calling ToString.
        /// </summary>
        public static string ToStringSafe<T>(this T value)
            where T : class
        {
            return value != null ? value.ToString() : null;
        }

        public static Func<T, T2> Fn<T, T2>(Func<T, T2> function)
        {
            return function;
        }
        public static Func<T, T2, T3> Fn<T, T2, T3>(Func<T, T2, T3> function)
        {
            return function;
        }
        public static Func<T, T2, T3, T4> Fn<T, T2, T3, T4>(Func<T, T2, T3, T4> function)
        {
            return function;
        }
        public static Func<T, T2> FnOver<T, T2>(this T a, Func<T, T2> function)
        {
            return function;
        }
        public static Func<T, T2, T3> FnOver<T, T2, T3>(this T a, Func<T, T2, T3> function)
        {
            return function;
        }
        public static Func<T, T2, T3, T4> FnOver<T, T2, T3, T4>(this T a, Func<T, T2, T3, T4> function)
        {
            return function;
        }
    }
}
