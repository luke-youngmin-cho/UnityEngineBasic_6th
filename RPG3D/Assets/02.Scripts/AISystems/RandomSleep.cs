using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.AISystems
{
    public class RandomSleep : Decorator
    {
        private float _sleepTimeMin;
        private float _sleepTimeMax;
        private float _sleepTime;
        private BehaviourTreeForCharacter _behaviourTree;

        public RandomSleep(BehaviourTreeForCharacter behaviourTree, float sleepTimeMin, float sleepTimeMax)
        {
            _behaviourTree = behaviourTree;
            _sleepTimeMin = sleepTimeMin;
            _sleepTimeMax = sleepTimeMax;
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
            _sleepTime = Random.Range(_sleepTimeMin, _sleepTimeMax);
            while (Time.time - timeMark < _sleepTime)
            {
                yield return null;
            }
            _behaviourTree.enabled = true;
        }
    }
}
