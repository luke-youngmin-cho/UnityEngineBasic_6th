using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public class Attack : MotionCommand
    {
        private int isComboFinishedParameterID;
        private int doComboParameterID;

        public Attack(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName) 
            : base(behaviourTree, animator, parameterName)
        {
            isComboFinishedParameterID = Animator.StringToHash("isComboFinished");
            doComboParameterID = Animator.StringToHash("doCombo");
        }

        public override IEnumerator<Result> Running()
        {
            IEnumerator<Result> baseEnumerator = base.Running();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            movement.mode = GameElements.MovementBase.Mode.RootMotion;
            InputManager.instance.mouse0Trigger = false;

            while (true)
            {
                if (InputManager.instance.mouse0Trigger &&
                    animator.GetBool(isComboFinishedParameterID) == false)
                {
                    animator.SetBool(doComboParameterID, true);

                    while (animator.GetNormalizedTime() < 1.0f)
                    {
                        yield return Result.Running;                        
                    }

                    animator.SetBool(doComboParameterID, false);
                }
                else
                {
                    if (animator.GetNormalizedTime() >= 1.0f)
                        break;
                }

                yield return Result.Running;
            }

            yield return Result.Success;
        }
    }
}