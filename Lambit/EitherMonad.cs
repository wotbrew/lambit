using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    /// <summary>
    /// Either monadic operations
    /// The monadic behaviour, is to map over the left side, until a right value is reached, at which point further computation is short-circuited.
    /// </summary>
    public static class EitherMonad
    {
        /// <summary>
        /// Monadic bind operation on a either
        /// Perform the computation if the either is in the 'left' state, otherwise shortcircuit.
        /// </summary>
        public static Either<T3, T2> Bind<T, T2, T3>(this Either<T, T2> either, Func<T, Either<T3, T2>> f)
        {
            if (either == null)
                throw new ArgumentNullException("either");

            if (either.Left.HasValue)
            {
                if (f == null)
                    throw new ArgumentNullException("f");

                return f(either.Left.OrDefault());
            }

            return either.MapLeft(x => default(T3));
        }

        /// <summary>
        /// Monadic bind operation on an either
        /// Perform the computation if the either is in the 'left' state, otherwise shortcircuit.
        /// </summary>
        public static Either<T3, T2> Select<T, T2, T3>(this Either<T, T2> either, Func<T, Either<T3, T2>> project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            return either.Bind(project);
        }

        /// <summary>
        /// Monadic bind + projection on an either
        /// used for LINQ support
        /// </summary>
        public static Either<T4, T2> SelectMany<T, T2, T3, T4>(this Either<T, T2> either,
            Func<T, Either<T3, T2>> select,
            Func<T, T3, T4> project)
        {
            if (select == null)
                throw new ArgumentNullException("select");
            if (project == null)
                throw new ArgumentNullException("project");

            return either.Bind(x => select(x).MapLeft(y => project(x, y)));
        }


        /// <summary>
        /// Monadic aggregation, short circuit the aggregation if the result becomes 'right' at any step.
        /// </summary>
        public static Either<T3, T2> AggregateM<T, T2, T3>(this IEnumerable<T> src, T3 seed, Func<T3, T, Either<T3, T2>> accum)
        {
            if (src == null)
                throw new ArgumentNullException("src");
            if (accum == null)
                throw new ArgumentNullException("accum");

            //imperative loop is for effiency with the lack of tail-recursion optimization
            var e = new Either<T3, T2>(seed);
            foreach (var i in src)
            {
                if (e.Right.HasValue)
                    break;

                e = e.Bind(st => accum(st, i));
            }
            return e;
        }


        /// <summary>
        /// Monadic right fold, short circuit the aggregation if the result becomes 'right' at any step.
        /// </summary>
        public static Either<T3, T2> FoldrM<T, T2, T3>(this IList<T> src, T3 seed, Func<T3, T, Either<T3, T2>> accum)
        {
            if (src == null)
                throw new ArgumentNullException("src");
            if (accum == null)
                throw new ArgumentNullException("accum");

            //imperative loop is for effiency with the lack of tail-recursion optimization
            var e = new Either<T3, T2>(seed);
            for (var i = src.Count - 1; i >= 0 && e.Left.HasValue; i--)
            {
                e = e.Bind(st => accum(st, src[i]));
            }
            return e;
        }

        /// <summary>
        /// Monadic aggregation, short circuit the aggregation if the result becomes 'right' at any step.
        /// </summary>
        public static Either<T3, T2> FoldM<T, T2, T3>(this IEnumerable<T> src, T3 seed, Func<T3, T, Either<T3, T2>> accum)
        {
            return AggregateM(src, seed, accum);
        }

        /// <summary>
        /// Monadic sum using a monoid instance, short circuit the sum of the result becomes 'right' at any step.
        /// </summary>
        public static Either<T, T2> SumM<T, T2>(this IEnumerable<Either<T, T2>> src, IMonoid<T> monoid)
        {
            if (monoid == null)
                throw new ArgumentNullException("monoid");

            return src.AggregateM(monoid.Identity, (n, a) => a.Map(x => monoid.Concat(n, x)));
        }


        /// <summary>
        /// Monadic select, short circuit the select if the result becomes 'right' at any step.
        /// </summary>
        public static Either<IList<T3>, T2> SelectM<T, T2, T3>(this IEnumerable<T> src,
            Func<T, Either<T3, T2>> project)
        {
            if (src == null)
                throw new ArgumentNullException("src");
            if (project == null)
                throw new ArgumentNullException("project");


            //imperative loop is for effiency with the lack of tail-recursion optimization
            var seq = src.Select(project);
            var lst = new List<T3>();
            foreach (var a in seq)
            {
                if (a.Right.HasValue)
                    return a.MapLeft(x => default(IList<T3>));

                lst.Add(a.Left.OrDefault());
            }

            return new Either<IList<T3>, T2>(lst as IList<T3>);
        }

    }
}
