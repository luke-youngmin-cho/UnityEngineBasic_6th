using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class Root : Behaviour, IChild
    {
        public Behaviour child { get; set; }

        public override Result Invoke()
        {
            return child.Invoke();
        }
    }
}
