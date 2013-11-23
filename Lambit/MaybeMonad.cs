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

    }

}
