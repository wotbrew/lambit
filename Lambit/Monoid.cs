using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public interface IMonoid<T>
    {
        T Identity {get;}
        T Concat(T a, T b);
    }

    public class FuncMonoid<T> : IMonoid<T>
    {
        private T _id;
        private Func<T, T, T> _concat;
        public FuncMonoid(T id, Func<T, T, T> concat)
        {
            if (concat == null)
                throw new ArgumentNullException("concat");

            _id = id;
            _concat = concat;
        }

        public T Identity
        {
            get { return _id; }
        }

        public T Concat(T a, T b)
        {
            return _concat(a, b);
        }
    }

    public static class Monoid
    {
        public static IMonoid<T> Create<T>(T id, Func<T, T, T> concat)
        {
            return new FuncMonoid<T>(id, concat);
        }

        public static readonly IMonoid<uint> UnsignedIntAdd = Create(0u, Num.Add);
        public static readonly IMonoid<uint> UnsignedIntMult = Create(1u, Num.Mult);
        public static readonly IMonoid<int> IntAdd = Create(0, Num.Add);
        public static readonly IMonoid<int> IntMult = Create(1, Num.Mult);

        public static readonly IMonoid<ulong> UnsignedLongAdd = Create(0uL, Num.Add);
        public static readonly IMonoid<ulong> UnsignedLongMult = Create(1uL, Num.Mult);
        public static readonly IMonoid<long> LongAdd = Create(0L, Num.Add);
        public static readonly IMonoid<long> LongMult = Create(1L, Num.Mult);

        public static readonly IMonoid<float> FloatAdd = Create(0f, Num.Add);
        public static readonly IMonoid<float> FloatMult = Create(1f, Num.Mult);

        public static readonly IMonoid<double> DoubleAdd = Create(0.0, Num.Add);
        public static readonly IMonoid<double> DoubleMult = Create(1.0, Num.Mult);

        public static readonly IMonoid<decimal> DecimalAdd = Create(0m, Num.Add);
        public static readonly IMonoid<decimal> DecimalMult = Create(1m, Num.Mult);

        public static IMonoid<IEnumerable<T>> Enumerable<T>()
        {
            return Create(System.Linq.Enumerable.Empty<T>(), System.Linq.Enumerable.Concat<T>);
        }
    }
}
