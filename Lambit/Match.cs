using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public interface IBaseMatch<T>
    {
        T Source { get; }
        Maybe<T2> UnsafeResolve<T2>();
    }

    public struct BaseMatch<T> : IBaseMatch<T>
    {
        private readonly T _src;
        public T Source { get { return _src; } }

        public BaseMatch(T src)
        {
            _src = src;
        }

        public Maybe<T2> UnsafeResolve<T2>()
        {
            return Maybe.Nothing;
        }
    }


    public struct PatternMatch<T, T2> : IBaseMatch<T>
    {
        private T _src;
        private Func<T, Maybe<T2>> _fun;

        public T Source
        {
            get { return _src; }
        }

        public PatternMatch (T src, Func<T, Maybe<T2>> match)
	    {
            _src = src;
            _fun = match;
	    }

        public Maybe<T2> Resolve()
        {
            return _fun != null ? _fun(_src) : Maybe.Nothing;
        }


        Maybe<T3> IBaseMatch<T>.UnsafeResolve<T3>()
        {
            return Resolve().As<T3>();
        }
    }

    public static class PatternMatch
    {
        public static PatternMatch<T, T2> Create<T, T2>(T src, Func<T, Maybe<T2>> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return new PatternMatch<T, T2>(src, f);
        }

        public static PatternMatch<T, T2> Comp<T, T2>(this IBaseMatch<T> match, Func<T, Maybe<T2>> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return Create(match.Source,
                x => match.UnsafeResolve<T2>().Alt(() => f(x)));
        }
        
        public static PatternMatch<T, T2> CaseNull<T, T2>(this IBaseMatch<T> match, Func<T2> f)
            where T : class
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp(x => x == null ? Maybe.Create(f()) : Maybe.Nothing);
        }

        public static T2 CaseElse<T, T2>(this IBaseMatch<T> match, Func<T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.UnsafeResolve<T2>().Resolve(() => f(match.Source));
        }
    }

}
