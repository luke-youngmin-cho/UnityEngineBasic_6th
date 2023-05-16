using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class Execution : Behaviour
    {
        private Func<Result> _execute;

        public Execution(Func<Result> execute)
        {
            _execute = execute;
        }

        public override Result Invoke()
        {
            return _execute.Invoke();
        }
    }
}
