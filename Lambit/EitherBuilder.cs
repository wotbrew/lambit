using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public struct EitherBuilder<T, T2>
    {
        public Either<T, T2> Left(T value)
        {
            return new Either<T, T2>(value);
        }
        public Either<T, T2> Right(T2 value)
        {
            return new Either<T, T2>(value);
        }
    }

    public static class EitherBuilder
    {
        public static EitherBuilder<T, T2> Of<T, T2>(T a, T2 b)
        {
            return new EitherBuilder<T, T2>();
        }
    }
}
