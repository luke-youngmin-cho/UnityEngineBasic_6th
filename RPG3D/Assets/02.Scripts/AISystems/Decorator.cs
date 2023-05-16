using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public abstract class Decorator : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        

        public override Result Invoke()
        {
            return Decorate(child);
        }

        protected abstract Result Decorate(Behaviour child);
        protected abstract Result Decorate(Result resultOfChild);
    }
}
