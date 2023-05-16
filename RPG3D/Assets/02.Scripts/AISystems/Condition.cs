using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class Condition : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        private Func<bool> _condition;

        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }

        public override Result Invoke()
        {
            if (_condition.Invoke())
                return child.Invoke();

            return Result.Failure;
        }
    }
}
