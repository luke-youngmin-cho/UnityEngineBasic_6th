using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.AISystems
{
    public class Move : MotionCommand
    {
        public Move(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName)
            : base(behaviourTree, animator, parameterName)
        {
        }

        public override IEnumerator<Result> Running()
        {
            IEnumerator<Result> baseEnumerator = base.Running();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            while (true)
            {
                yield return Result.Running;
            }
        }
    }
}
