using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMvvmTester.Model
{
    public class EventManager
    {
        public delegate void TestDelegate();
        public event TestDelegate TestEvent;
    }
}
