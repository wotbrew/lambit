using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class Lazy
    {
        /// <summary>
        /// Static constructor for the lazy-initialized values
        /// </summary>
        public static Lazy<T> Create<T>(Func<T> thunk)
        {
            return new Lazy<T>(thunk);
        }

        /// <summary>
        /// Map over a lazy value
        /// </summary>
        public static Lazy<T2> Map<T, T2>(this Lazy<T> lazy, Func<T, T2> f)
        {
            if (lazy == null)
                throw new ArgumentNullException("lazy");
            if (f == null)
                throw new ArgumentNullException("f");

            return Create(() => f(lazy.Value));
        }
        /// <summary>
        /// Map over a lazy value (synonym for map)
        /// </summary>
        public static Lazy<T2> Select<T, T2>(this Lazy<T> lazy, Func<T, T2> f)
        {
            return Map(lazy, f);
        }

        /// <summary>
        /// Monadic bind operation.
        /// </summary>
        public static Lazy<T2> Bind<T, T2>(this Lazy<T> lazy, Func<T, Lazy<T2>> f)
        {
            return Create(() => lazy.Select(f).Value.Value);
        }
        /// <summary>
        /// Monadic bind operation. 
        /// </summary>
        public static Lazy<T2> SelectMany<T, T2>(this Lazy<T> lazy,
            Func<T, Lazy<T2>> f)
        {
            return Bind(lazy, f);
        }

        /// <summary>
        /// Monadic bind operation. 
        /// Overload required for linq support.
        /// </summary>
        public static Lazy<T3> SelectMany<T, T2, T3>(this Lazy<T> lazy,
            Func<T, Lazy<T2>> f,
            Func<T, T2, T3> projector)
        {
            if (projector == null)
                throw new ArgumentNullException("projector");
            return Bind(lazy, f).Map(x => projector(lazy.Value, x));
        }

    }
}
