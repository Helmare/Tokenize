using System;

namespace Tokenize.StateMachine
{
    /// <summary>
    ///     An exception for when the state machine fails. Contains information
    ///     about how it failed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StateMachineException<T> : Exception
    {
        /// <summary>
        ///     The bit which caused the state machine to fail.
        /// </summary>
        public T UnexpectedBit { get; }

        /// <summary>
        ///     The position of the bit.
        /// </summary>
        public int Position { get; }

        /// <summary>
        ///     The state which it failed.
        /// </summary>
        public int State { get; }

        public StateMachineException(T unexpectedBit, int position, int state)
            : base("Unexpected " + typeof(T).Name + " \"" + unexpectedBit.ToString() + "\" at state " + state + ", position " + position + ".")
        {
            this.UnexpectedBit = unexpectedBit;
            this.Position = position;
            this.State = state;
        }
    }
}
