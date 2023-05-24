using RPG.GameElements;
using RPG.GameElements.Casters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public class Fall : MotionCommand
    {
        private Transform _transform;
        private GroundDetector _groundDetector;
        private Vector3 _startPos;
        private float _landingDistance = 1.0f;
        private Land _land;

        public Fall(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName) 
            : base(behaviourTree, animator, parameterName)
        {
            _transform = behaviourTree.owner.transform;
            _groundDetector = behaviourTree.owner.GetComponent<GroundDetector>();
            _land = new Land(behaviourTree, animator, "doLand");
        }

        public override IEnumerator<Result> Running()
        {
            IEnumerator<Result> baseEnumerator = base.Running();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            movement.mode = MovementBase.Mode.Manual;
            _startPos = _transform.position;

            while (true)
            {
                if (_groundDetector.TryCastGround(out RaycastHit hit, 0.1f))
                {
                    if (Mathf.Abs(_transform.position.y - _startPos.y) < _landingDistance)
                    {
                        yield return Result.Success;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    yield return Result.Running;
                }
            }

            behaviourTree.Interrupt(_land);
            yield return Result.Running;
        }
    }
}
