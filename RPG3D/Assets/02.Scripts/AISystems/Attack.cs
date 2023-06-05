using RPG.InputSystems; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

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

            behaviourTree.owner.transform.LookAt(behaviourTree.target.transform);
            movement.mode = GameElements.MovementBase.Mode.RootMotion;
            InputManager.instance.mouse0Trigger = false;
            while (true)
            {
                // 이전 콤보 상태가 정상적으로 끝났을때, 입력이 없음에도 불구하고
                // 그다음 콤보가 바로 이어서 나가면 안되기때문에 
                // 콤보 파라미터를 끈다
                if (animator.isPreviousStateFinished &&
                    animator.GetBool(doComboParameterID))
                {
                    animator.SetBool(doComboParameterID, false);
                }

                // 마우스왼쪽이 트리거가 되었고, 콤보가 아직 남았으면 콤보파라미터를 켠다.
                if (InputManager.instance.mouse0Trigger &&
                    animator.GetBool(isComboFinishedParameterID) == false)
                {
                    animator.SetBool(doComboParameterID, true);
                }
                else
                {
                    // 현재 콤보 애니메이션 시간이 다되었으면 Attack 명령을 종료한다.
                    if (animator.GetNormalizedTime() >= 1.0f &&
                        animator.GetBool(doComboParameterID) == false)
                        break;
                }

                yield return Result.Running;
            }

            yield return Result.Success;
        }
    }
}