using MicroMvvmBase.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMvvmTester.Model
{
    public class Foo : BindableModelBase
    {
        /// <summary>
        /// Observable Property
        /// </summary>
        private string _myThing;
        public string MyThing
        {
            get => _myThing;
            set => Set(ref _myThing, value);
        }

        /// <summary>
        /// Observable Property
        /// </summary>
        private bool _canExecute;
        public bool CanExecute
        {
            get => _canExecute;
            set => Set(ref _canExecute, value);
        }

        public void TestEvent()
        {
        }
    }    
}
