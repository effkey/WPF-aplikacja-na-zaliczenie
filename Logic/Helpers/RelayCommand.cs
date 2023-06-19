﻿using System;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app.Logic.Helpers
{
    public class RelayCommand : ICommand
    {
        private Action mAction;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action action)
        {
            mAction = action;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            mAction();
        }
    }
}
