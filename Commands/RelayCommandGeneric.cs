using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicroMvvmBase.Commands
{
    public class RelayCommandGeneric<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<T> methodToExecute;
        private Func<bool> canExecuteEvaluator;

        public RelayCommandGeneric(Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommandGeneric(Action<T> methodToExecute, object p)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke((T)parameter);
        }
    }
}
