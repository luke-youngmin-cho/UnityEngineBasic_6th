using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public abstract class MotionCommand : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        protected AnimatorWrapper animator;
        protected int animatorParameterID;
        protected BehaviourTreeForCharacter behaviourTree;


        public MotionCommand(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName)
        {
            this.behaviourTree = behaviourTree;
            this.animator = animator;
            animatorParameterID = Animator.StringToHash(parameterName);
        }


        public override Result Invoke()
        {
            Debug.Log($"[{this.GetType()}] : Invoked");

            if (animator.GetBool(animatorParameterID) == false &&
                animator.isPreviousMachineFinished &&
                animator.isPreviousStateFinished)
            {
                animator.SetBool(animatorParameterID, true);

                behaviourTree.runningFSM = Running();
                return Result.Running;
            }

            return Result.Failure;
        }

        public virtual IEnumerator<Result> Running()
        {
            animator.SetBool(behaviourTree.currentAnimatorParameterID, false);
            behaviourTree.currentAnimatorParameterID = animatorParameterID;
            Debug.Log($"[{this.GetType()}] : Start running...");
            yield return Result.Running;
        }
    }
}
