using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class RandomSelector : Composite
    {
        public override Result Invoke()
        {
            Result result = Result.Failure;
            foreach (Behaviour child in children.OrderBy(c => Guid.NewGuid()))
            {
                result = child.Invoke();
                if (result != Result.Failure)
                    return result;
            }

            return result;
        }
    }
}
