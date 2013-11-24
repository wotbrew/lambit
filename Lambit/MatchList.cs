using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    /// <summary>
    /// Pattern matching and destructuring operations over lists.
    /// </summary>
    public static class MatchList
    {
        /// <summary>
        /// Create an empty pattern match for the given list.
        /// </summary>
        public static IBaseMatch<IList<T>> Match<T>(this IList<T> src)
        {
            return new BaseMatch<IList<T>>(src);
        }

        /// <summary>
        /// Match an empty sequence.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseEmpty<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count == 0 ? Maybe.Create(f()) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a null or empty sequence.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseNullOrEmpty<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs == null || xs.Count == 0 ? Maybe.Create(f()) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of exactly 1 element.
        /// </summary>
        public static PatternMatch<IList<T>, T2> Case<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count == 1 ? Maybe.Create(f(xs[0])) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of exactly 2 elements.
        /// </summary>
        public static PatternMatch<IList<T>, T2> Case<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count == 2 ? Maybe.Create(f(xs[0], xs[1])) : Maybe.Nothing);
        }


        /// <summary>
        /// Match a sequence of exactly 3 elements.
        /// </summary>
        public static PatternMatch<IList<T>, T2> Case<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count == 3 ? Maybe.Create(f(xs[0], xs[1], xs[2])) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of exactly 4 elements.
        /// </summary>
        public static PatternMatch<IList<T>, T2> Case<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, T, T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count == 4 ? Maybe.Create(f(xs[0], xs[1], xs[2], xs[3])) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of at least 1 element. The rest of the sequence being available to the match expression.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseCons<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, IEnumerable<T>, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count > 0 ? Maybe.Create(f(xs[0], xs.Slice(1))) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of at least 2 elements. The rest of the sequence being available to the match expression.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseCons<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, IEnumerable<T>, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count > 1 ? Maybe.Create(f(xs[0], xs[1], xs.Slice(2))) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of at least 3 elements. The rest of the sequence being available to the match expression.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseCons<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, T, IEnumerable<T>, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count > 2 ? Maybe.Create(f(xs[0], xs[1], xs[2], xs.Slice(3))) : Maybe.Nothing);
        }

        /// <summary>
        /// Match a sequence of at least 4 elements. The rest of the sequence being available to the match expression.
        /// </summary>
        public static PatternMatch<IList<T>, T2> CaseCons<T, T2>(this IBaseMatch<IList<T>> match,
            Func<T, T, T, T, IEnumerable<T>, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Count > 3 ? Maybe.Create(f(xs[0], xs[1], xs[2], xs[3], xs.Slice(4))) : Maybe.Nothing);
        }
    }
}
