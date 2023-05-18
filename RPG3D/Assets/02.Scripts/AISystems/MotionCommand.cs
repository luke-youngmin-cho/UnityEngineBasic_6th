using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.AISystems
{
    public abstract class MotionCommand : Behaviour, IChild
    {
        public Behaviour child { get; set; }
        private AnimatorWrapper _animator;
        private Func<bool> _condition;
        private int _animatorParameterID;
        private BehaviourTreeForCharacter _behaviourTree;


        public MotionCommand(BehaviourTreeForCharacter behaviourTree, AnimatorWrapper animator, Func<bool> condition, string parameterName)
        {
            _behaviourTree = behaviourTree;
            _animator = animator;
            _condition = condition;
            _animatorParameterID = Animator.StringToHash(parameterName);
        }


        public override Result Invoke()
        {
            if (_animator.GetBool(_animatorParameterID) == false &&
                _animator.isPreviousMachineFinished &&
                _animator.isPreviousStateFinished &&
                _condition.Invoke())
            {
                _animator.SetBool(_animatorParameterID, true);
                
                return Result.Running;
            }

            return Result.Failure;
        }

        public struct FSM : IEnumerator<Result>
        {
            public Result Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
