using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.GameElements;

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

            movement.mode = MovementBase.Mode.RootMotion;
        }
    }
}
