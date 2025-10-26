using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maio10.Controllers
{
    public class Cmd : ICommand

    {
        private Action<Object> _execute;
        private Predicate<Object> _canExecute;

        public Cmd(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object parameter)
        {
           return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
           _execute.Invoke(parameter);
        }
    }
}
