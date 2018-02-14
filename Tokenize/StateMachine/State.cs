using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenize.StateMachine
{
    /// <summary>
    ///     A State class which uses state transition(s) to perform actions.
    /// </summary>
    /// <typeparam name="T">The type of stream that this class will be used for.</typeparam>
    public class State<T>
    {
        /// <summary>
        ///     Gets a list of transitions.
        /// </summary>
        public List<StateTransition<T>> Transitions { get; } = new List<StateTransition<T>>();

        /// <summary>
        ///     Add a transition to the list.
        /// </summary>
        /// <param name="transition">The transition to be added.</param>
        /// <returns>This TransitionalState for chaining.</returns>
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
        /// <returns>This TransitionalState for chaining.</returns>
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
        public State<T> AddTransition(int nextState, T validBit, StateAction<T> action)
        {
            return AddTransition(new StateTransition<T>(nextState, validBit, action));
        }
        /// <summary>
        ///     Adds a transition to the list.
        /// </summary>
        /// <param name="nextState">The nex state if the transition is valid.</param>
        /// <param name="validator">The delegate to check if the bit is valid.</param>
        /// <returns>The TransitionState for chaining.</returns>
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
        public State<T> AddTransition(int nextState, T validBit)
        {
            return AddTransition(nextState, validBit, null);
        }
    }
}
