using MicroMvvmBase.Base;
using MicroMvvmBase.Commands;
using MicroMvvmTester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMvvmTester.ViewModel
{
    public class MainWindowViewModel : BindableModelBase
    {
        /// <summary>
        /// Observable Property
        /// </summary>
        private bool _canExecuteLocal;
        public bool CanExecuteLocal
        {
            get => _canExecuteLocal;
            set
            {
                Set(ref _canExecuteLocal, value);
                if (MyFoo is not null)
                    MyFoo.CanExecute = value;
            }
        }

        // Events testing
        EventManager eventManager;
        // Weak ref
        WeakReference fooRef;

        // Property
        private Foo _myFoo;
        public Foo MyFoo
        {
            get => _myFoo;
            set => Set(ref _myFoo, value);
        }

        // Command
        private RelayCommandGeneric<bool?> _command;
        public RelayCommandGeneric<bool?> Command => _command = _command ?? new RelayCommandGeneric<bool?>(CommandAction, () => MyFoo.CanExecute, MyFoo, "CanExecute");

        // Command
        private RelayCommandGeneric<bool?> _commandGCFoo;
        public RelayCommandGeneric<bool?> CommandGCFoo => _commandGCFoo = _commandGCFoo ?? new RelayCommandGeneric<bool?>(CommandGCFooAction, null);

        private void CommandGCFooAction(bool? obj)
        {
            System.Diagnostics.Debug.WriteLine($"Is Alive Start: {fooRef.IsAlive}");

            MyFoo = null;
            System.Diagnostics.Debug.WriteLine($"Is Alive after null: {fooRef.IsAlive}");
            eventManager = null;
            GC.Collect();

            System.Diagnostics.Debug.WriteLine($"Is Alive after GC: {fooRef.IsAlive}");
        }

        private void CommandAction(bool? obj)
        {
            System.Diagnostics.Debug.WriteLine("Action executed");
        }

        public MainWindowViewModel()
        {
            MyFoo = new Foo();

            fooRef = new WeakReference(MyFoo);

            eventManager = new EventManager();
            eventManager.TestEvent += MyFoo.TestEvent;
        }
    }
}
