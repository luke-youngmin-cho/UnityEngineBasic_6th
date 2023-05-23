using System;
using System.Collections.Generic;
using RPG.GameElements;

namespace RPG.AISystems
{
    public class Land : MotionCommand
    {
        public Land(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName) 
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

            while (animator.GetNormalizedTime() < 1.0f)
            {
                yield return Result.Running;
            }
            yield return Result.Success;
        }
    }
}
