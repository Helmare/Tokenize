using System.Collections.Generic;

namespace Tokenize.StateMachine
{
    /// <summary>
    ///     A State class which uses state transition(s) to perform actions.
    /// </summary>
    /// <typeparam name="T">The type of stream that this class will be used for.</typeparam>
    public class State<T>
    {
        /// <summary>
        ///     The index associated with the FiniteStateMachine.
        /// </summary>
        public int Index { get; }
        /// <summary>
        ///     Gets or sets whether this state is a valid ending state.
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        ///     Gets a list of transitions.
        /// </summary>
        public List<StateTransition<T>> Transitions { get; } = new List<StateTransition<T>>();


        internal State(int index)
        {
            this.Index = index;
        }

        /// <summary>
        ///     Add a transition to the list.
        /// </summary>
        /// <param name="transition">The transition to be added.</param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(StateTransition<T> transition)
        {
            Transitions.Add(transition);
            return this;
        }
        /// <summary>
        ///     Add a transition to the list.
        /// </summary>
        /// <param name="nextState">The next state if the transition is valid.</param>
        /// <param name="validator">The delegate to check if the bit is valid.</param>
        /// <param name="action">The action to perform when the transition is valid.</param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, BitValidator<T> validator, StateAction<T> action)
        {
            return AddTransition(new StateTransition<T>(nextState, validator, action));
        }
        /// <summary>
        ///     Adds a state transition which is only 
        ///     valid when the bit is equal to the 
        ///     <code>validBit</code>.
        /// </summary>
        /// <param name="nextState">The next state if the transition is valid.</param>
        /// <param name="validBit"></param>
        /// <param name="action">The action to perform when the transition is valid.</param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, T validBit, StateAction<T> action)
        {
            return AddTransition(nextState, (bit) => bit.Equals(validBit), action);
        }
        /// <summary>
        ///     Adds s state transition which is only
        ///     valid when the bit is part of the
        ///     <code>validBits</code> set.
        /// </summary>
        /// <param name="nextState">The next state if the transition is valid.</param>
        /// <param name="validBits"></param>
        /// <param name="action">The action to perform when the transition is valid.</param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, IEnumerable<T> validBits, StateAction<T> action)
        {
            return AddTransition(nextState, (bit) =>
            {
                foreach(T validBit in validBits)
                {
                    if (bit.Equals(validBit)) return true;
                }
                return false;
            }, action);
        }
        /// <summary>
        ///     Adds a transition to the list.
        /// </summary>
        /// <param name="nextState">The nex state if the transition is valid.</param>
        /// <param name="validator">The delegate to check if the bit is valid.</param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, BitValidator<T> validator)
        {
            return AddTransition(nextState, validator, null);
        }
        /// <summary>
        ///     Adds a state transition which is only 
        ///     valid when the bit is equal to the 
        ///     <code>validBit</code>.
        /// </summary>
        /// <param name="nextState">The next state if the transition is valid.</param>
        /// <param name="validBit"></param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, T validBit)
        {
            return AddTransition(nextState, validBit, null);
        }
        /// <summary>
        ///     Adds s state transition which is only
        ///     valid when the bit is part of the
        ///     <code>validBits</code> set.
        /// </summary>
        /// <param name="nextState">The next state if the transition is valid.</param>
        /// <param name="validBits"></param>
        /// <returns>The current State for chaining.</returns>
        public State<T> AddTransition(int nextState, IEnumerable<T> validBits)
        {
            return AddTransition(nextState, validBits, null);
        }
    }
}
