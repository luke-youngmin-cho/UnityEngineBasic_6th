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
        private float _landingDistance = 2.0f;
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

            _startPos = _transform.position;

            while (true)
            {
                if (_groundDetector.TryCastGround(out RaycastHit hit))
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

            while (_land.Invoke() != Result.Running)
            {
                yield return Result.Running;
            }

            behaviourTree.runningFSM = _land.Running();
            yield return Result.Running;
        }
    }
}
