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

        public override IEnumerator<Result> Running()
        {
            IEnumerator<Result> baseEnumerator = base.Running();

            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            movement.mode = MovementBase.Mode.RootMotion;

            while (behaviourTree.target != null &&
                   Vector3.Distance(_owner.position, behaviourTree.target.transform.position) > _endDistance)
            {
                Vector3 forward = (behaviourTree.target.transform.position - _owner.position);
                forward = new Vector3(forward.x, 0.0f, forward.y);
                _owner.rotation = Quaternion.LookRotation(forward, Vector3.up);
                yield return Result.Running;
            }

            yield return Result.Success;
        }
    }
}
