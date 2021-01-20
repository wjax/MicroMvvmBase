using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicroMvvmBase.Commands
{
    public class RelayCommandGeneric<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<T> _methodToExecute;
        private Func<bool> _canExecuteEvaluator;
        private string _propertyChangedName;

        public RelayCommandGeneric(Action<T> methodToExecute, Func<bool> canExecuteEvaluator, INotifyPropertyChanged eventHost, string propertyChangedName) 
            : this(methodToExecute, canExecuteEvaluator)
        {
            eventHost.PropertyChanged += OnReevaluateCanExecute;
            _propertyChangedName = propertyChangedName;
        }

        public RelayCommandGeneric(Action<T> methodToExecute, object p)
            : this(methodToExecute, null)
        {
        }

        public RelayCommandGeneric(Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;

            // Borrowed from Prism Library
            TypeInfo genericTypeInfo = typeof(T).GetTypeInfo();

            // RelayCommandGenerics allows object or Nullable<>.  
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
            {
                if ((!genericTypeInfo.IsGenericType) || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
                {
                    throw new InvalidCastException("T is not Nullable nor a class");
                }
            }
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
            _methodToExecute.Invoke((T)parameter);
        }
    }
}
