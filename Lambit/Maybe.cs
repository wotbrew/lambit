using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    public struct Nothing
    {
    }
    public static class Maybe
    {
        public static readonly Nothing Nothing = default(Nothing);
        public static Maybe<T> Create<T>(T value)
        {
            return new Maybe<T>(value);
        }

    }

    public struct Maybe<T>
    {
        private readonly T _val;
        private readonly bool _hasValue;

        public bool HasValue { get { return _hasValue; } }

        public Maybe(T val)
        {
            _val = val;
            _hasValue = true;
        }

        public T Resolve(Func<T> otherwise)
        {
            if (!HasValue)
            {
                if (otherwise == null)
                    throw new ArgumentNullException("otherwise");

                return otherwise();
            }

            return _val;
        }

        public T OrDefault()
        {
            return _val;
        }


        public static implicit operator Maybe<T>(Nothing n)
        {
            return default(Maybe<T>);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || (obj is Nothing && !HasValue);
        }

        public override int GetHashCode()
        {
            if (HasValue)
                return base.GetHashCode();
            else
                return Maybe.Nothing.GetHashCode();
        }
    }


    public static class MaybeFunctor
    {
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

        public static Maybe<T2> Select<T, T2>(this Maybe<T> maybe, Func<T, T2> project)
        {
            return maybe.Map(project);
        }
    }

    public static class MaybeMonad
    {
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

        public static Maybe<T2> SelectMany<T, T2>(this Maybe<T> maybe, Func<T, Maybe<T2>> select)
        {
            if (select == null)
                throw new ArgumentNullException("select");

            return maybe.Bind(select);
        }

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
