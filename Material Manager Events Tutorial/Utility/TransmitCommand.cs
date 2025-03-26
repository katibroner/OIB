#region Copyright

// ASM Device Manager - Copyright (C) ASM AS GmbH & Co. KG
// All rights reserved.
// 
// The software and associated documentation supplied hereunder are
// the proprietary information of ASM AS and are supplied subject to license terms.

#endregion

using System;
using System.Windows.Input;

namespace MaterialManagerEventsTutorial.Utility
{
    /// <summary>
    /// The command class implementation
    /// </summary>
    public class TransmitCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the can execute delegate.
        /// </summary>
        /// <value>
        /// The can execute delegate.
        /// </value>
        public Predicate<object> CanExecuteDelegate { get; set; }

        /// <summary>
        /// Gets or sets the execute delegate.
        /// </summary>
        /// <value>
        /// The execute delegate.
        /// </value>
        public Action<object> ExecuteDelegate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmitCommand" /> class.
        /// </summary>
        /// <param name="executeDelegate">The execute delegate.</param>
        /// <param name="canExecuteDelegate">The can execute delegate.</param>
        public TransmitCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate = null)
        {
            ExecuteDelegate = executeDelegate;
            CanExecuteDelegate = canExecuteDelegate;
        }

        #region ICommand

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(Object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);

            return true;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(Object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
            }
        }

        #endregion ICommand
    }
}
