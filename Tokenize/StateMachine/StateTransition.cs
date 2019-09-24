namespace Tokenize.StateMachine
{
    /// <summary>
    ///     A structure with infoirmation about a state transition.
    /// </summary>
    /// <typeparam name="T">The type of stream that this struct will be used for.</typeparam>
    public struct StateTransition<T>
    {
        /// <summary>
        ///     Gets the next state if this transition is valid.
        /// </summary>
        public int NextState { get; }
        /// <summary>
        ///     Gets the BitValidator for this transition.
        /// </summary>
        public BitValidator<T> IsValidBit { get; }
        /// <summary>
        ///     Gets the StateAction for this transition.
        /// </summary>
        public StateAction<T> Action { get; }


        public StateTransition(int nextState, BitValidator<T> validator, StateAction<T> action)
        {
            this.NextState = nextState;
            this.IsValidBit = validator;
            this.Action = action;
        }

        /// <summary>
        ///     A state transition which is only valid when the bit
        ///     is equal to the <code>validBit</code>.
        /// </summary>
        /// <param name="validBit"></param>
        /// <param name="action"></param>
        public StateTransition(int nextState, T validBit, StateAction<T> action)
        {
            this.NextState = nextState;
            this.IsValidBit = (bit) => bit.Equals(validBit);
            this.Action = action;
        }
    }

    /// <summary>
    ///     A delgate used for checking whether a bit is valid for
    ///     a state transition.
    /// </summary>
    /// <typeparam name="T">The type of stream that this method will be used for.</typeparam>
    /// <param name="bit">A bit of information from the stream.</param>
    /// <returns>Wehther the bit was valid</returns>
    public delegate bool BitValidator<T>(T bit);

    /// <summary>
    ///     A delegate used in state transitions to perform an action
    ///     when the bit is valid for the transition.
    /// </summary>
    /// <typeparam name="T">The type of stream that this method will be used for.</typeparam>
    /// <param name="bit">A bit of information from the stream.</param>
    public delegate void StateAction<T>(T bit);
}
