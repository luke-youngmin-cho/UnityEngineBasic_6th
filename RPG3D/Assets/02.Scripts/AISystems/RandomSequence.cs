using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class RandomSequence : Composite
    {
        public override Result Invoke()
        {
            Result result = Result.Success;
            foreach (Behaviour child in children.OrderBy(c => Guid.NewGuid()))
            {
                result = child.Invoke();
                if (result != Result.Success)
                    return result;
            }

            return result;
        }
    }
}
