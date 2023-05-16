using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public interface IChild
    {
        Behaviour child { get; set; }
    }
}
