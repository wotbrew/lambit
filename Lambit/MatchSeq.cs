using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class MatchSeq
    {
        public static IBaseMatch<IEnumerable<T>> Match<T>(this IEnumerable<T> src)
        {
            return new BaseMatch<IEnumerable<T>>(src);
        }


        public static PatternMatch<IEnumerable<T>, T2> CaseEmpty<T, T2>(this IBaseMatch<IEnumerable<T>> match,
            Func<T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && !xs.Any() ? Maybe.Create(f()) : Maybe.Nothing);
        }

        public static PatternMatch<IEnumerable<T>, T2> CaseNullOrEmpty<T, T2>(this IBaseMatch<IEnumerable<T>> match,
            Func<T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs == null || !xs.Any() ? Maybe.Create(f()) : Maybe.Nothing);
        }

        public static PatternMatch<IEnumerable<T>, T2> CaseCons<T, T2>(this IBaseMatch<IEnumerable<T>> match,
            Func<T, IEnumerable<T>, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp((xs) =>
                xs != null && xs.Any() ? Maybe.Create(f(xs.First(), xs.Skip(1))) : Maybe.Nothing);
        }

    }
}
