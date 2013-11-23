using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    /// <summary>
    /// Haskell style functor operations on Maybe
    /// </summary>
    public static class MaybeFunctor
    {
        /// <summary>
        /// Map a maybe using the supplied function.
        /// Similar to 'Select' on sequences.
        /// </summary>
        public static Maybe<T2> Map<T, T2>(this Maybe<T> maybe, Func<T, T2> f)
        {
            if (maybe.HasValue)
            {
                if (f == null)
                    throw new ArgumentNullException("f");

                return Maybe.Create(f(maybe.OrDefault()));
            }

            return Maybe.Nothing;
        }

        /// <summary>
        /// Map a maybe using the supplied function.
        /// Similar to 'Select' on sequences.
        /// Synonym for 'Map'
        /// </summary>
        public static Maybe<T2> Select<T, T2>(this Maybe<T> maybe, Func<T, T2> project)
        {
            return maybe.Map(project);
        }

        /// <summary>
        /// Resolve either the value in the maybe applied to the given function 'f',
        /// or the default value of the return type of 'f'.
        /// </summary>
        public static T2 OrDefault<T, T2>(this Maybe<T> maybe, Func<T, T2> f)
        {
            return maybe.Map(f).OrDefault();
        }

    }
}
