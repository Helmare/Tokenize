using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenize.StateMachine
{
    /// <summary>
    ///     A class for holding information about the 
    ///     result of a state machine evaluation.
    /// </summary>
    public class MachineResult
    {
        /// <summary>
        ///     Gets the state the StateMachine ended on.
        /// </summary>
        public int State { get; }
        /// <summary>
        ///     Gets the error message.
        /// </summary>
        public string Error { get; }
        /// <summary>
        ///     Gets whether there is an error.
        /// </summary>
        public bool HasError => Error != null;

        private MachineResult(int state, string error)
        {
            this.State = state;
            this.Error = error;
        }

        /// <summary>
        ///     Gets a successful MachineResult.
        /// </summary>
        /// <param name="state">The state the machine ended on.</param>
        /// <returns>The MachineResult generated.</returns>
        public static MachineResult Success(int state)
        {
            return new MachineResult(state, null);
        }

        /// <summary>
        ///     Gets a failure MachineResult.
        /// </summary>
        /// <param name="state">The state the machine ended on.</param>
        /// <param name="error">The error message.</param>
        /// <returns>The MachineResult generated.</returns>
        public static MachineResult Failed(int state, string error)
        {
            if (error == null) throw new ArgumentException();
            return new MachineResult(state, error);
        }
    }
}
