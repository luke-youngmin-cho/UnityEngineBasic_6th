using RPG.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.AISystems
{
    public class Follow : MotionCommand
    {
        private Transform _owner;
        private float _endDistance;
        public Follow(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName, float endDistance)
            : base(behaviourTree, animator, parameterName)
        {
            _owner = behaviourTree.owner.transform;
            _endDistance = endDistance;
        }

        public override Result Invoke()
        {
            if (animator.GetBool(animatorParameterID) &&
                animator.isPreviousMachineFinished &&
                animator.isPreviousStateFinished)
            {
                behaviourTree.runningFSM = Running();
                return Result.Running;
            }

            return Result.Failure;
        }

        public override IEnumerator<Result> Running()
        {
            movement.mode = MovementBase.Mode.RootMotion;
            movement.SetMove(0.0f, 1.0f, 1.0f);

            while (behaviourTree.target != null &&
                   Vector3.Distance(_owner.position, behaviourTree.target.transform.position) > _endDistance)
            {
                _owner.transform.LookAt(behaviourTree.target.transform);
                Debug.Log("Following...");
                yield return Result.Running;
            }

            yield return Result.Success;
        }
    }
}
