using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    /// <summary>
    /// A pattern match, describing some sequence of potential destructuring, conditional operations and their resulting actions.
    /// </summary>
    public interface IBaseMatch<T>
    {
        /// <summary>
        /// The value you are matching over.
        /// </summary>
        T Source { get; }

        /// <summary>
        /// Attempt to resolve the pattern match to a value of type T2. 
        /// This is an implementation detail, you should probably never need to call this.
        /// </summary>
        Maybe<T2> UnsafeResolve<T2>();
    }

    /// <summary>
    /// A pattern match, describing some sequence of potential destructuring, conditional operations and their resulting actions.
    /// </summary>
    public struct BaseMatch<T> : IBaseMatch<T>
    {
        private readonly T _src;
        /// <summary>
        /// The value you are matching over.
        /// </summary>
        public T Source { get { return _src; } }

        public BaseMatch(T src)
        {
            _src = src;
        }

        /// <summary>
        /// Attempt to resolve the pattern match to a value of type T2. 
        /// This is an implementation detail, you should probably never need to call this.
        /// </summary>
        public Maybe<T2> UnsafeResolve<T2>()
        {
            return Maybe.Nothing;
        }
    }


    /// <summary>
    /// A pattern match, describing some sequence of potential destructuring, conditional operations and their resulting actions.
    /// </summary>
    public struct PatternMatch<T, T2> : IBaseMatch<T>
    {
        private T _src;
        private Func<T, Maybe<T2>> _fun;

        /// <summary>
        /// The value you are matching over.
        /// </summary>
        public T Source
        {
            get { return _src; }
        }

        /// <summary>
        /// Create a pattern match, with a single conditional transformation.
        /// </summary>
        public PatternMatch (T src, Func<T, Maybe<T2>> match)
	    {
            _src = src;
            _fun = match;
	    }

        /// <summary>
        /// Resolve the match, if any match condition succeeds, return the value of the match expression.
        /// </summary>
        public Maybe<T2> Resolve()
        {
            return _fun != null ? _fun(_src) : Maybe.Nothing;
        }


        /// <summary>
        /// Attempt to resolve the pattern match to a value of type T2. 
        /// This is an implementation detail, you should probably never need to call this.
        /// </summary>
        Maybe<T3> IBaseMatch<T>.UnsafeResolve<T3>()
        {
            return Resolve().As<T3>();
        }
    }

    /// <summary>
    /// Pattern matching/destructureing operations.
    /// </summary>
    public static class PatternMatch
    {
        /// <summary>
        /// Create a pattern match over the value src using the conditional function f.
        /// </summary>
        public static PatternMatch<T, T2> Create<T, T2>(T src, Func<T, Maybe<T2>> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return new PatternMatch<T, T2>(src, f);
        }

        /// <summary>
        /// Composition of an existing match and a further conditional function.
        /// </summary>
        public static PatternMatch<T, T2> Comp<T, T2>(this IBaseMatch<T> match, Func<T, Maybe<T2>> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return Create(match.Source,
                x => match.UnsafeResolve<T2>().Alt(() => f(x)));
        }
        
        /// <summary>
        /// Matches when the source value is null.
        /// </summary>
        public static PatternMatch<T, T2> CaseNull<T, T2>(this IBaseMatch<T> match, Func<T2> f)
            where T : class
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.Comp(x => x == null ? Maybe.Create(f()) : Maybe.Nothing);
        }

        /// <summary>
        /// Resolve the pattern match by providing an alternative.
        /// </summary>
        public static T2 CaseElse<T, T2>(this IBaseMatch<T> match, Func<T, T2> f)
        {
            if (f == null)
                throw new ArgumentNullException("f");

            return match.UnsafeResolve<T2>().Resolve(() => f(match.Source));
        }
    }

}
