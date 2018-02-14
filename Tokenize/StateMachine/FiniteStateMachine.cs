using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tokenize.StateMachine
{
    /// <summary>
    ///     A class which mimics a finite state machine by using an object and it's methods.
    /// </summary>
    public class FiniteStateMachine<T>
    {
        /// <summary>
        ///     Gets a list of states.
        /// </summary>
        public List<State<T>> States { get; } = new List<State<T>>();

        /// <summary>
        ///     Adds a state to the machine.
        /// </summary>
        /// <param name="state">The state to be added.</param>
        public void AddState(State<T> state)
        {
            States.Add(state);
        }

        /// <summary>
        ///     Adds an empty state to the machine.
        /// </summary>
        /// <returns>The state created.</returns>
        public State<T> AddState()
        {
            State<T> state = new State<T>();
            AddState(state);
            return state;
        }

        /// <summary>
        ///     Runs the machine.
        /// </summary>
        /// <param name="stream">The stream to itterate over.</param>
        /// <param name="callActions">
        ///     Whether to call the action of the transitions.
        ///     This is usefull for testing whether a stream is
        ///     valid.
        /// </param>
        /// <returns>The state the machine finished on.</returns>
        public int Run(IEnumerable<T> stream, bool callActions)
        {
            int state = 0;
            int pos = 1;
            foreach(T bit in stream)
            {
                // Execute the next transition.
                int nextState = -1;
                foreach (StateTransition<T> transition in States[state].Transitions)
                {
                    if (transition.Validator(bit))
                    {
                        transition.Action?.Invoke(bit);
                        nextState = transition.NextState;
                        break;
                    }
                }

                // Throw error if no transition is found.
                if (nextState == -1)
                {
                    throw new StateMachineException<T>(bit, pos, state);
                }

                // Update info.
                state = nextState;
                pos++;
            }
            return state;
        }
    }
}
