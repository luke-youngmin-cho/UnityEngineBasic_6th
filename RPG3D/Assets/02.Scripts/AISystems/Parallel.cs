using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public enum Policy
    {
        RequireNone,
        RequireOne,
        RequireAll,
    }

    public class Parallel : Composite
    {
        private Policy _successPolicy;

        public Parallel(Policy successPolicy)
        {
            _successPolicy = successPolicy;
        }

        public override Result Invoke()
        {
            Result result = Result.Failure;
            int successCount = 0;
            foreach (Behaviour child in children)
            {
                Result tmp = child.Invoke();
                if (tmp == Result.Success)
                    successCount++;
                else if (tmp == Result.Running)
                    result = Result.Running;
            }

            if (result == Result.Running)
                return result;

            switch (_successPolicy)
            {
                case Policy.RequireNone:
                    return Result.Success;
                case Policy.RequireOne:
                    return successCount >= 1 ? Result.Success : Result.Failure;
                case Policy.RequireAll:
                    return successCount == children.Count ? Result.Success : Result.Failure;
                default:
                    throw new Exception($"[BT.Parallel] : Wrong success policy");
            }
        }
    }
}
