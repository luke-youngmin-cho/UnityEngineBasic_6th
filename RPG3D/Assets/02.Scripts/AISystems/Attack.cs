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
                // ���� �޺� ���°� ���������� ��������, �Է��� �������� �ұ��ϰ�
                // �״��� �޺��� �ٷ� �̾ ������ �ȵǱ⶧���� 
                // �޺� �Ķ���͸� ����
                if (animator.isPreviousStateFinished &&
                    animator.GetBool(doComboParameterID))
                {
                    animator.SetBool(doComboParameterID, false);
                }

                // ���콺������ Ʈ���Ű� �Ǿ���, �޺��� ���� �������� �޺��Ķ���͸� �Ҵ�.
                if (InputManager.instance.mouse0Trigger &&
                    animator.GetBool(isComboFinishedParameterID) == false)
                {
                    animator.SetBool(doComboParameterID, true);
                }
                else
                {
                    // ���� �޺� �ִϸ��̼� �ð��� �ٵǾ����� Attack ����� �����Ѵ�.
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