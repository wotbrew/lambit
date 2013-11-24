using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class MaybeEither
    {
        /// <summary>
        /// Take either the value in the maybe, or the value provided.
        /// </summary>
        public static Either<T, T2> OrValue<T, T2>(this Maybe<T> maybe, T2 value)
        {
            if (maybe.HasValue)
                return new Either<T, T2>(maybe.OrDefault());
            
            return new Either<T, T2>(value);
        }

        /// <summary>
        /// Take either the value in the maybe, or the value provided by the function 'otherwise'.
        /// </summary>
        public static Either<T, T2> Resolve<T, T2>(this Maybe<T> maybe, Func<T2> otherwise)
        {
            if (maybe.HasValue)
                return new Either<T, T2>(maybe.OrDefault());

            if (otherwise == null)
                throw new ArgumentNullException("otherwise");

            return new Either<T, T2>(otherwise());
        }
    }
}
