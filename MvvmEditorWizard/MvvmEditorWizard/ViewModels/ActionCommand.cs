using System;
using System.Windows.Input;

namespace MvvmEditorWizard.ViewModels
{
  /// <summary>
  /// Adapter for user input
  /// </summary>
  public class ActionCommand : ICommand
  {
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;

    /// <summary>
    /// Creates a new instance
    /// </summary>
    /// <param name="executeAction">A delegate that is called when command dis executed.</param>
    public ActionCommand(Action<object> executeAction)
     : this(executeAction, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ActionCommand class
    /// Creates a new command.
    /// </summary>
    /// <param name="executeAction">The execution logic</param>
    /// <param name="canExecuteCheck">The execution status logic</param>
    public ActionCommand(Action<object> executeAction, Predicate<object> canExecuteCheck)
    {
      _execute = executeAction;
      _canExecute = canExecuteCheck;
    }

    /// <summary>
    /// An event to raise when the CanExecute value is changed
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Defines if command can be executed
    /// </summary>
    /// <param name="parameter">the parameter that represents the validation method</param>
    /// <returns>true if the command can be executed</returns>
    public bool CanExecute(object parameter)
    {
      return _canExecute != null && _canExecute(parameter);
    }

    /// <summary>
    /// Execute the encapsulated command
    /// </summary>
    /// <param name="parameter">the parameter that represents the execution method</param>
    public void Execute(object parameter)
    {
      _execute(parameter);
    }
  }
}