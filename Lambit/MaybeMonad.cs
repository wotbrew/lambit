using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    /// <summary>
    /// Haskell style monad operations on Maybe (inc LINQ support)
    /// </summary>
    public static class MaybeMonad
    {
        /// <summary>
        /// Monadic bind operation on a maybe
        /// </summary>
        public static Maybe<T2> Bind<T, T2>(this Maybe<T> maybe, Func<T, Maybe<T2>> f)
        {
            if (maybe.HasValue)
            {
                if (f == null)
                    throw new ArgumentNullException("f");

                return f(maybe.OrDefault());
            }

            return Maybe.Nothing;
        }

        /// <summary>
        /// Monadic bind operation on a maybe
        /// </summary>
        public static Maybe<T2> SelectMany<T, T2>(this Maybe<T> maybe, Func<T, Maybe<T2>> select)
        {
            if (select == null)
                throw new ArgumentNullException("select");

            return maybe.Bind(select);
        }

        /// <summary>
        /// Monadic bind + projection on a maybe
        /// used for LINQ support
        /// </summary>
        public static Maybe<T3> SelectMany<T, T2, T3>(this Maybe<T> maybe,
            Func<T, Maybe<T2>> select,
            Func<T, T2, T3> project)
        {
            if (select == null)
                throw new ArgumentNullException("select");
            if (project == null)
                throw new ArgumentNullException("project");

            return maybe.Bind(x => select(x).Map(y => project(x, y)));
        }


        /// <summary>
        /// Monadic aggregation, short circuit the aggregation if the result becomes maybe at any step.
        /// </summary>
        public static Maybe<T2> AggregateM<T, T2>(this IEnumerable<T> src, T2 seed, Func<T2, T, Maybe<T2>> accum)
        {
            if (src == null)
                return Maybe.Nothing;
            if (accum == null)
                throw new ArgumentNullException("accum");

            //imperative loop is for effiency with the lack of tail-recursion optimization
            var m = Maybe.Create(seed);
            foreach (var i in src)
            {
                if(!m.HasValue)
                    break;

                m = m.Bind(st => accum(st, i));
            }
            return m;
        }


        /// <summary>
        /// Monadic right fold, short circuit the aggregation if the result becomes nothing at any step.
        /// </summary>
        public static Maybe<T2> FoldrM<T, T2>(this IList<T> src, T2 seed, Func<T2, T, Maybe<T2>> accum)
        {
            if (src == null)
                return Maybe.Nothing;
            if (accum == null)
                throw new ArgumentNullException("accum");

            //imperative loop is for effiency with the lack of tail-recursion optimization
            var m = Maybe.Create(seed);
            for (var i = src.Count -1 ; i > 0 && m.HasValue; i--)
            {
                m = m.Bind(st => accum(st, src[i]));
            }
            return m;
        }

        /// <summary>
        /// Monadic aggregation, short circuit the aggregation if the result becomes nothing at any step.
        /// </summary>
        public static Maybe<T2> FoldM<T, T2>(this IEnumerable<T> src, T2 seed, Func<T2, T, Maybe<T2>> accum)
        {
            return AggregateM(src, seed, accum);
        }

        /// <summary>
        /// Monadic sum using a monoid instance, short circuit the sum of the result becomes nothing at any step.
        /// </summary>
        public static Maybe<T> SumM<T>(this IEnumerable<Maybe<T>> src, IMonoid<T> monoid)
        {
            if (monoid == null)
                throw new ArgumentNullException("monoid");

            return src.AggregateM(monoid.Identity, (n, a) => a.Map(x => monoid.Concat(n, x)));
        }

        /// <summary>
        /// Monadic select, short circuit the select if the result becomes nothing at any step.
        /// </summary>
        public static Maybe<IList<T2>> SelectM<T, T2>(this IEnumerable<T> src,
            Func<T, Maybe<T2>> project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            if (src == null)
                return Maybe.Nothing;

            //imperative loop is for effiency with the lack of tail-recursion optimization
            var x = src.Select(project);
            var lst = new List<T2>();
            foreach (var a in x)
            {
                if (!a.HasValue)
                    return Maybe.Nothing;

                lst.Add(a.OrDefault());
            }

            return Maybe.Create(lst as IList<T2>);
        }

    }

}
