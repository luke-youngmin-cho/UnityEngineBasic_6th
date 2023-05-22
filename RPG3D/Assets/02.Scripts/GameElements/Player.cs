using RPG.AISystems;
using RPG.GameElements.Casters;
using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private BehaviourTreeForCharacter _behavourTree;

    private void Start()
    {
        AnimatorWrapper animator = GetComponent<AnimatorWrapper>();
        GroundDetector groundDetector = GetComponent<GroundDetector>();
        _behavourTree = new BehaviourTreeForCharacter(gameObject);
        _behavourTree.StartBuild()
            .Selector()
                .Selector()
                    .Condition(() => groundDetector.isDetected == false)
                        .Fall()
                    .Condition(() => groundDetector.isDetected == true)
                        .Move();

        new Move(_behavourTree, animator, "doMove").Invoke();
        _behavourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");

        int horizontalParameterID = Animator.StringToHash("h");
        int verticalParameterID = Animator.StringToHash("v");
        InputManager.instance.RegisterAxisAction("Horizontal", (value) => animator.SetFloat(horizontalParameterID, value));
        InputManager.instance.RegisterAxisAction("Vertical", (value) => animator.SetFloat(verticalParameterID, value));
    }

    private void Update()
    {
        _behavourTree.Run();
    }
}
