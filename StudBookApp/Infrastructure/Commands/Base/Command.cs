using System;
using System.Windows.Input;

namespace StudBookApp.Infrastructure.Commands.Base;

public abstract class Command : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);
}

