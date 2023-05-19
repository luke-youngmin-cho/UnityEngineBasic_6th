using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.AISystems
{
    public class Jump : MotionCommand
    {
        private Rigidbody _rb;
        private float _force = 5.0f;

        public Jump(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, string parameterName)
            : base(behaviourTree, animator, parameterName)
        {
            _rb = behaviourTree.owner.GetComponent<Rigidbody>();
        }

        public override IEnumerator<Result> Running()
        {
            IEnumerator<Result> baseEnumerator = base.Running();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            _rb.velocity = new Vector3(_rb.velocity.x, 0.0f, _rb.velocity.y);
            _rb.AddForce(Vector3.up * _force, ForceMode.Impulse);
            while (true)
            {
                if (_rb.velocity.y < 0)
                    yield return Result.Success;
                else
                    yield return Result.Running;
            }
        }
    }
}
