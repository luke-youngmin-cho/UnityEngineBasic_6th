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
        [SerializeField] protected float seekRadius;
        [SerializeField] protected float seekAngle;
        [SerializeField] protected float seekAngleDelta;
        [SerializeField] protected LayerMask seekTargetMask;
        [SerializeField] protected Vector3 seekOffset;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float followLimit;
        [SerializeField] protected float thinkTimeMin;
        [SerializeField] protected float thinkTimeMax;


        private void Start()
        {
            GroundDetector groundDetector = GetComponent<GroundDetector>();
            EnemyMovement movement = GetComponent<EnemyMovement>();

            _behaviourTree = new BehaviourTreeForCharacter(gameObject);
            _behaviourTree.StartBuild()
                .Selector()
                    .Condition(() => groundDetector.isDetected == false)
                        .Fall()
                    .Condition(() => groundDetector.isDetected)
                        .Selector()
                            .Sequence()
                                .Seek(seekRadius, seekAngle, seekAngleDelta, seekTargetMask, seekOffset)
                                .Selector()
                                    .Condition(() => Vector3.Distance(transform.position, _behaviourTree.target.transform.position) < attackRange)
                                        .Attack()
                                    .Follow(followLimit)
                                .ExitCurrentComposite()
                            .ExitCurrentComposite()
                            .RandomSelector()
                                .Sequence()
                                    .Execution(() =>
                                    {
                                        float angleY = Random.Range(0.0f, 360.0f);
                                        transform.eulerAngles = new Vector3(0.0f, angleY, 0.0f);
                                        return Result.Success;
                                    })
                                    .Execution(() =>
                                    {
                                        movement.SetMove(0.0f, 1.0f, 0.5f);
                                        return Result.Success;
                                    })
                                    .RandomSleep(thinkTimeMin, thinkTimeMax)
                                        .Move()
                                .ExitCurrentComposite()
                                .Sequence()
                                    .Execution(() =>
                                    {
                                        movement.SetMove(0.0f, 0.0f, 0.0f);
                                        return Result.Success;
                                    })
                                    .RandomSleep(thinkTimeMin, thinkTimeMax)
                                        .Move();

            AnimatorWrapper animator = GetComponent<AnimatorWrapper>();
            Move move = new Move(_behaviourTree, animator, "doMove");
            move.Invoke();
            _behaviourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");
        }

        private void Update()
        {
            _behaviourTree.Run();
        }
    }
}