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

        Move move = new Move(_behavourTree, animator, "doMove");
        Jump jump = new Jump(_behavourTree, animator, "doJump");
        int jumpParameterID = Animator.StringToHash("doJump");

        InputManager.instance.RegisterPressAction(KeyCode.Space, () =>
        {
            if (_behavourTree.currentAnimatorParameterID != jumpParameterID &&
                groundDetector.TryCastGround(out RaycastHit hit, 1.0f))
            {
                _behavourTree.Interrupt(jump);
            }
        });

        move.Invoke();
        _behavourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");
    }

    private void Update()
    {
        _behavourTree.Run();
    }
}
