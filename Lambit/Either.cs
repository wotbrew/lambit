using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    /// <summary>
    /// An either represents one and only one of 2 possible values.
    /// These values can have distinct types.
    /// The monad defines 'error' style short-circuiting when a 'right' value is recieved.
    /// This is opposite behavior to haskell, where the monad short circuits on the 'left' value.
    /// </summary>
    public class Either<T, T2>
    {
        /// <summary>
        /// Take the left value, if the either holds the left value. If it does not, it is guaranteed to hold the right value.
        /// </summary>
        public Maybe<T> Left { get; private set; }
        /// <summary>
        /// Take the right value, if the either right the left value. If it does not, it is guaranteed to hold the left value.
        /// </summary>
        public Maybe<T2> Right { get; private set; }

        /// <summary>
        /// Create a 'left' either using the value provided. 
        /// </summary>
        public Either(T left)
        {
            Left = Maybe.Create(left);
        }

        /// <summary>
        /// Create a 'right' either using the value provided. 
        /// </summary>
        public Either(T2 right)
        {
            Right = Maybe.Create(right);
        }


        /// <summary>
        /// Resolve the either to a value of a single type.
        /// </summary>
        public T3 Resolve<T3>(Func<T, T3> left,
            Func<T2, T3> right)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            if (Left.HasValue)
                return left(Left.OrDefault());

            return right(Right.OrDefault());
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            var either = obj as Either<T, T2>;
            if (either != null)
                return either.Left.Equals(Left) && either.Right.Equals(Right);

            return false;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Right.GetHashCode();
        }

        public override string ToString()
        {
            return Left.HasValue ? "Left: " + Left.ToString() : Right.ToString();
        }

        public static bool operator ==(Either<T, T2> a, Either<T, T2> b)
        {
            return object.Equals(a, b);
        }
        public static bool operator !=(Either<T, T2> a, Either<T, T2> b)
        {
            return !(a == b);
        }
    }

    /// <summary>
    /// An either represents one and only one of 2 possible values.
    /// These values can have distinct types.
    /// The monad defines 'error' style short-circuiting when a 'right' value is recieved.
    /// This is opposite behavior to haskell, where the monad short circuits on the 'left' value.
    /// </summary>
    public static class Either
    {
        /// <summary>
        /// Create a 'left' either using the value provided. 
        /// </summary>
        public static Either<T, T2> Left<T, T2>(T value)
        {
            return new Either<T, T2>(value);
        }
        /// <summary>
        /// Create a 'right' either using the value provided. 
        /// </summary>
        public static Either<T, T2> Right<T, T2>(T2 value)
        {
            return new Either<T, T2>(value);
        }

        /// <summary>
        /// Join an either where both sides would be the same type the value within, whether it is 'left' or 'right'.
        /// </summary>
        public static T Join<T>(this Either<T, T> either)
        {
            return either.Resolve(Fun.Id, Fun.Id);
        }
    }
}
