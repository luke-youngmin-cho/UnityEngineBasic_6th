using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class Repeat : Decorator
    {
        private int _times;
        private int _count;

        public Repeat(int times)
        {
            _times = times;
        }

        public override Result Invoke()
        {
            _count = 0;
            return base.Invoke();
        }

        protected override Result Decorate(Behaviour child)
        {
            Result result = Result.Failure;
            while (_count < _times)
            {
                result = child.Invoke();
                _count++;
            }
            return result;
        }
        protected override Result Decorate(Result resultOfChild)
        {
            throw new NotImplementedException();
        }
    }
}
