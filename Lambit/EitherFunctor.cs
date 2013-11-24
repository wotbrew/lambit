using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class EitherFunctor
    {
        /// <summary>
        /// Map both sides of the either (i.e either left or right) using the functions provided.
        /// </summary>
        public static Either<T3, T4> MapBoth<T, T2, T3, T4>(this Either<T, T2> either, Func<T, T3> leftf, Func<T2, T4> rightf)
        {
            if(leftf == null)
                throw new ArgumentNullException("leftf");
            if(rightf == null)
                throw new ArgumentNullException("rightf");
            if(either == null)
                throw new ArgumentNullException("either");

            if(either.Left.HasValue)
                return new Either<T3,T4>(either.Left.OrDefault(leftf));

            return new Either<T3,T4>(either.Right.OrDefault(rightf));
        }


        /// <summary>
        /// Map a function over the left value of the either, if it is there.
        /// </summary>
        public static Either<T3, T2> MapLeft<T, T2, T3>(this Either<T, T2> either, Func<T, T3> f)
        {
            return either.MapBoth(f, Fun.Id);
        }

        /// <summary>
        /// Map a function over the right value of the either, if it is there.
        /// </summary>
        public static Either<T, T3> MapRight<T, T2, T3>(this Either<T, T2> either, Func<T2, T3> f)
        {
            return either.MapBoth(Fun.Id, f);
        }

        /// <summary>
        /// Map a function over the left value of the either, if it is there.
        /// Synonym for MapLeft
        /// </summary>
        public static Either<T3, T2> Map<T, T2, T3>(this Either<T, T2> either, Func<T, T3> f)
        {
            return either.MapBoth(f, Fun.Id);
        }

        /// <summary>
        /// Map a function over the left value of the either, if it is there.
        /// Synonym for MapLeft
        /// </summary>
        public static Either<T3, T2> Select<T, T2, T3>(this Either<T, T2> either, Func<T, T3> f)
        {
            return either.MapBoth(f, Fun.Id);
        }
    }
}
