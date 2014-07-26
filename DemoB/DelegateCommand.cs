﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Commands
{
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteAction { get; set; }

        public Func<object, bool> CanExecuteFunc { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc == null) return true;
            return this.CanExecuteFunc(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (ExecuteAction == null) return;
            this.ExecuteAction(parameter);
        }
    }
}
