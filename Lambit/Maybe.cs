using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{

    public interface IMaybe
    {
        bool HasValue { get; }
        object OrDefaultUntyped();
    }

    /// <summary>
    /// Represents the absence of value
    /// can be implicitly converted to a Maybe
    /// Can serve as a unit-type or void type.
    /// </summary>
    public struct Nothing : IMaybe
    {
        bool IMaybe.HasValue
        {
            get { return false; }
        }

        object IMaybe.OrDefaultUntyped()
        {
            return null;
        }
    }

    /// <summary>
    /// An option type describing the potential absence of a value.
    /// </summary>
    public struct Maybe<T> : IMaybe
    {
        private readonly T _val;
        private readonly bool _hasValue;

        /// <summary>
        /// Does the maybe hold the value
        /// (N.B) the value itself could still be null...
        /// </summary>
        public bool HasValue { get { return _hasValue; } }

        /// <summary>
        /// Construct a new Maybe with the value provided.
        /// </summary>
        public Maybe(T val)
        {
            _val = val;
            _hasValue = true;
        }

        /// <summary>
        /// Resolve the maybe value by supply an alternative if the value is not there.
        /// </summary>
        public T Resolve(Func<T> otherwise)
        {
            if (!_hasValue)
            {
                if (otherwise == null)
                    throw new ArgumentNullException("otherwise");

                return otherwise();
            }

            return _val;
        }
        
        /// <summary>
        /// Like Resolve but without the thunk, simply returns either the value in the maybe
        /// or the value provided.
        /// </summary>
        public T OrValue(T value)
        {
            if (_hasValue)
                return _val;

            return value;
        }

        /// <summary>
        /// Returns the value in the maybe, or the default value of type T.
        /// </summary>
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


        object IMaybe.OrDefaultUntyped()
        {
            return _val;
        }
    }


    /// <summary>
    /// An option type describing the potential absence of a value.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Static instance of Nothing.
        /// </summary>
        public static readonly Nothing Nothing = default(Nothing);
        
        /// <summary>
        /// Create a maybe using the value provided.
        /// </summary>
        public static Maybe<T> Create<T>(T value)
        {
            return new Maybe<T>(value);
        }
        /// <summary>
        /// Create a maybe using the value provided.
        /// Synonym for create
        /// </summary>
        public static Maybe<T> Just<T>(T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Flatten a Maybe 'inside' a Maybe.
        /// </summary>
        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe)
        {
            return maybe.Bind(F.Id);
        }

        /// <summary>
        /// Take the maybe if it has a value, otherwise take the maybe supplied by the 
        /// alternative function.
        /// </summary>
        public static Maybe<T> Alt<T>(this Maybe<T> maybe, Func<Maybe<T>> alternative)
        {
            return maybe.HasValue ? maybe : alternative();
        }

        /// <summary>
        /// Attempt to cast the value in the maybe.
        /// </summary>
        public static Maybe<T> As<T>(this IMaybe maybe)
        {
            var def = maybe.OrDefaultUntyped();
            return maybe.HasValue && def is T ? Maybe.Create((T)def) : Maybe.Nothing;
        }

        /// <summary>
        /// Take the value in the maybe, if the predicate returns true for the value.
        /// </summary>
        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return maybe.OrDefault(predicate) ? maybe : Maybe.Nothing;
        }
    }


}
