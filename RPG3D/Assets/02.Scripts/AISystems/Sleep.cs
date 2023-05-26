using System;
using System.Collections;
using UnityEngine;

namespace RPG.AISystems
{
    public class Sleep : Decorator
    {
        private float _sleepTime;
        private BehaviourTreeForCharacter _behaviourTree;

        public Sleep(BehaviourTreeForCharacter behaviourTree, float sleepTime)
        {
            _behaviourTree = behaviourTree;
            _sleepTime = sleepTime;
        }

        public override Result Invoke()
        {
            return Decorate(child.Invoke());
        }

        protected override Result Decorate(Behaviour child)
        {
            throw new NotImplementedException();
        }

        protected override Result Decorate(Result resultOfChild)
        {
            _behaviourTree.enabled = false;
            _behaviourTree.owner
                .GetComponent<MonoBehaviour>()
                .StartCoroutine(EnableBehaviourTreeAfterSeconds());
            return resultOfChild;
        }

        private IEnumerator EnableBehaviourTreeAfterSeconds()
        {
            float timeMark = Time.time;
            while (Time.time - timeMark < _sleepTime)
            {
                yield return null;
            }
            _behaviourTree.enabled = true;
        }
    }
}
