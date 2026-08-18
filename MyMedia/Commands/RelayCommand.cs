using System.Windows.Input;

namespace MyMedia.Commands;

public class RelayCommand : ICommand
{
    private Action _execute;
    private Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null!) =>
        (_execute, _canExecute) = (execute, canExecute);

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => _execute();
}
