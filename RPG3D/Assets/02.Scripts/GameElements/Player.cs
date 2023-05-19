using RPG.AISystems;
using RPG.GameElements.Casters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private BehaviourTreeForCharacter _behavourTree;

    private void Start()
    {
        GroundDetector groundDetector = GetComponent<GroundDetector>();
        _behavourTree = new BehaviourTreeForCharacter(gameObject);
        _behavourTree.StartBuild()
            .Selector()
                .Selector()
                    .Condition(() => groundDetector.isDetected == false)
                        .Fall()
                    .Condition(() => groundDetector.isDetected == true)
                        .Move();

        GetComponent<Animator>().SetBool("doMove", true);
    }

    private void Update()
    {
        _behavourTree.Run();
    }
}
