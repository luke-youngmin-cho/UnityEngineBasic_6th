using RPG.AISystems;
using RPG.GameElements.Casters;
using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private BehaviourTreeForCharacter _behaviourTree;

    private void Start()
    {
        AnimatorWrapper animator = GetComponent<AnimatorWrapper>();
        GroundDetector groundDetector = GetComponent<GroundDetector>();
        _behaviourTree = new BehaviourTreeForCharacter(gameObject);
        _behaviourTree.StartBuild()
            .Selector()
                .Selector()
                    .Condition(() => groundDetector.isDetected == false)
                        .Fall()
                    .Condition(() => groundDetector.isDetected == true)
                        .Move();

        Move move = new Move(_behaviourTree, animator, "doMove");
        Jump jump = new Jump(_behaviourTree, animator, "doJump");
        Attack attack = new Attack(_behaviourTree, animator, "doAttack");
        int jumpParameterID = Animator.StringToHash("doJump");
        int attackParameterID = Animator.StringToHash("doAttack");

        InputManager.instance.RegisterPressAction(KeyCode.Space, () =>
        {
            if (_behaviourTree.currentAnimatorParameterID != jumpParameterID &&
                groundDetector.TryCastGround(out RaycastHit hit, 0.1f))
            {
                _behaviourTree.Interrupt(jump);
            }
        });

        InputManager.instance.onMouse0Triggered += () =>
        {
            if (_behaviourTree.currentAnimatorParameterID != attackParameterID)
            {
                _behaviourTree.Interrupt(attack);
            }
        };


        move.Invoke();
        _behaviourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");
    }

    private void Update()
    {
        _behaviourTree.Run();
    }
}
