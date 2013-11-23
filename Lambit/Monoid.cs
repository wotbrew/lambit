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

        public static readonly IMonoid<int> IntAdd = Create(0, Num.Add);
        public static readonly IMonoid<int> IntMult = Create(1, Num.Mult);

        public static IMonoid<IEnumerable<T>> SeqConcat<T>()
        {
            return Create(Enumerable.Empty<T>(), Enumerable.Concat<T>);
        }
    }
}
