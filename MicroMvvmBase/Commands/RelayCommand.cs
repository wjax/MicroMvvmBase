using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MicroMvvmBase.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _methodToExecute;
        private Func<bool> _canExecuteEvaluator;
        private string _propertyChangedName;

        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator, INotifyPropertyChanged eventHost, string propertyChangedName)
            : this(methodToExecute, canExecuteEvaluator)
        {
            eventHost.PropertyChanged += OnReevaluateCanExecute;
            _propertyChangedName = propertyChangedName;
        }

        public RelayCommand(Action<object> methodToExecute, object p)
            : this(methodToExecute, null)
        {
        }

        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        private void OnReevaluateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(_propertyChangedName))
                CanExecuteChanged?.Invoke(this, null);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = _canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            _methodToExecute.Invoke(parameter);
        }
    }
}
