using RPG.AISystems;
using RPG.GameElements.Casters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public class Enemy : Character
    {
        private BehaviourTreeForCharacter _behaviourTree;

        private void Start()
        {
            //GroundDetector groundDetector = GetComponent<GroundDetector>();
            //
            //_behaviourTree = new BehaviourTreeForCharacter(gameObject);
            //_behaviourTree.StartBuild()
            //    .Selector()
            //        .Condition(() => groundDetector.isDetected == false)
            //            .Fall()
            //        .Condition(() => groundDetector.isDetected)
            //            .Selector()
            //                .Sequence()
        }
    }
}