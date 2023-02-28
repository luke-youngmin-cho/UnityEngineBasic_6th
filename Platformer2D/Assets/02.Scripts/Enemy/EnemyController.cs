using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    public enum StateType
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die
    }
    public StateType current;
    // 어떤 상태일때 어떤 로직을 수행해야하는지에 대한 사전
    private Dictionary<StateType, IEnumerator<int>> _workflows = new Dictionary<StateType, IEnumerator<int>>();
    // 어떤 조건일때 어떤 상태를 수행할 수 있는지에 대한 사전
    private Dictionary<StateType, Func<bool>> _conditions = new Dictionary<StateType, Func<bool>>();

    #region AI
    private enum AI
    {
        Idle,
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        FollowTarget,
        AttackTarget
    }
    [SerializeField] private AI _ai;
    [SerializeField] private bool _aiAutoFollow;
    [SerializeField] private float _aiDetectRange = 2.0f;
    [SerializeField] private bool _aiAttackEnable = false;
    [SerializeField] private float _aiAttackRange = 0.5f;
    [SerializeField] private float _aiBehaviourTimeMin = 0.1f;
    [SerializeField] private float _aiBehaviourTimeMax = 2.0f;
    [SerializeField] private float _aiBehaviourTimer;
    [SerializeField] private LayerMask _aiTargetMask;
    #endregion

    #region Workflows
    public struct IdleWorkflow : IEnumerator<int>
    {
        public int Current => _current;

        object IEnumerator.Current => _current;

        private int _current;
        private EnemyController _controller;

        public IdleWorkflow(EnemyController controller)
        {
            _current = 0;
            _controller = controller;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            switch (_current)
            {
                case 0:
                    {
                        _controller._movable = false;
                        _controller._animator.Play("Idle");
                        _current++;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _current = 0;
        }
    }

    public struct MoveWorkflow : IEnumerator<int>
    {
        public int Current => _current;

        object IEnumerator.Current => _current;

        private int _current;
        private EnemyController _controller;

        public MoveWorkflow(EnemyController controller)
        {
            _current = 0;
            _controller = controller;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            switch (_current)
            {
                case 0:
                    {
                        _controller._movable = true;
                        _controller._animator.Play("Move");
                        _current++;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _current = 0;
        }
    }

    public struct AttackWorkflow : IEnumerator<int>
    {
        public int Current => _current;

        object IEnumerator.Current => _current;

        private int _current;
        private EnemyController _controller;

        public AttackWorkflow(EnemyController controller)
        {
            _current = 0;
            _controller = controller;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            switch (_current)
            {
                case 0:
                    {
                        _controller._movable = false;
                        _controller._animator.Play("Attack");
                        _current++;
                    }
                    break;
                case 1:
                    if (_controller._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        _controller.current = StateType.Idle;
                        return false;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _current = 0;
        }
    }

    public struct HurtWorkflow : IEnumerator<int>
    {
        public int Current => _current;

        object IEnumerator.Current => _current;

        private int _current;
        private EnemyController _controller;

        public HurtWorkflow(EnemyController controller)
        {
            _current = 0;
            _controller = controller;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            switch (_current)
            {
                case 0:
                    {
                        _controller._movable = false;
                        _controller._animator.Play("Hurt");
                        _current++;
                    }
                    break;
                case 1:
                    {
                        if (_controller._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        {
                            _controller.current = StateType.Idle;
                            return false;
                        }
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _current = 0;
        }
    }

    public struct DieWorkflow : IEnumerator<int>
    {
        public int Current => _current;

        object IEnumerator.Current => _current;

        private int _current;
        private EnemyController _controller;

        public DieWorkflow(EnemyController controller)
        {
            _current = 0;
            _controller = controller;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            switch (_current)
            {
                case 0:
                    {
                        _controller._movable = false;
                        _controller._animator.Play("Die");
                        _current++;
                    }
                    break;
                case 1:
                    {
                        if (_controller._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        {
                            _current++;
                        }
                    }
                    break;
                case 2:
                    // nothing to do.
                    break;
                default:
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _current = 0;
        }
    }
    #endregion

    // 방향
    private const int DIRECTION_LEFT = -1;
    private const int DIRECTION_RIGHT = 1;
    public int direction
    {
        get => _direction;
        set
        {
            if (value == DIRECTION_LEFT)
            {
                _direction = DIRECTION_LEFT;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else if (value == DIRECTION_RIGHT)
            {
                _direction = DIRECTION_RIGHT;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    private int _direction;

    [SerializeField] private bool _moveEnable = true;
    [SerializeField] private float _moveSpeed = 1.0f;
    private bool _movable;

    public bool ChangeState(StateType newState)
    {
        if (_conditions[newState].Invoke())
        {
            current = newState;
            _workflows[current].Reset();
            return true;
        }

        return false;
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        InitializeWorkflows();
    }

    private void Update()
    {
        UpdateAI();

        if (_workflows[current].MoveNext() == false)
            _workflows[current].Reset();
    }

    private void FixedUpdate()
    {
        if (_moveEnable && _movable)
            _rb.MovePosition(_rb.position + _direction * Vector2.right * _moveSpeed * Time.fixedDeltaTime);
    }

    private void InitializeWorkflows()
    {
        _workflows.Add(StateType.Idle, new IdleWorkflow(this));
        _workflows.Add(StateType.Move, new MoveWorkflow(this));
        _workflows.Add(StateType.Attack, new AttackWorkflow(this));
        _workflows.Add(StateType.Hurt, new HurtWorkflow(this));
        _workflows.Add(StateType.Die, new DieWorkflow(this));

        _conditions.Add(StateType.Idle, () => true);
        _conditions.Add(StateType.Move, () => true);
        _conditions.Add(StateType.Attack, () => current == StateType.Idle || current == StateType.Move);
        _conditions.Add(StateType.Hurt, () => true);
        _conditions.Add(StateType.Die, () => true);
    }

    private void UpdateAI()
    {
        Collider2D target = Physics2D.OverlapCircle(_rb.position, _aiDetectRange, _aiTargetMask);

        //타겟 감지
        if (target != null)
        {
            // 타겟 공격범위내에 있는지 체크
            if (_aiAttackEnable &&
                Vector2.Distance(_rb.position, target.transform.position) <= _aiAttackRange)
            {
                _ai = AI.AttackTarget;
            }
            else
            {
                _ai = AI.FollowTarget;
            }
            _aiBehaviourTimer = 0;
        }  
        else if (_aiBehaviourTimer <= 0)
        {
            _ai = AI.Think;
        }

        switch (_ai)
        {
            case AI.Idle:
                break;
            case AI.Think:
                {
                    _ai = (AI)Random.Range((int)AI.TakeARest, (int)AI.MoveRight + 1);
                    _aiBehaviourTimer = Random.Range(_aiBehaviourTimeMin, _aiBehaviourTimeMax);

                    if (_ai == AI.TakeARest)
                        ChangeState(StateType.Idle);
                    else
                        ChangeState(StateType.Move);
                }
                break;
            case AI.TakeARest:
                {
                    if (_aiBehaviourTimer > 0)
                    {
                        _aiBehaviourTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.MoveLeft:
                {
                    if (_aiBehaviourTimer > 0)
                    {
                        direction = DIRECTION_LEFT;
                        _aiBehaviourTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.MoveRight:
                {
                    if (_aiBehaviourTimer > 0)
                    {
                        direction = DIRECTION_RIGHT;
                        _aiBehaviourTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.FollowTarget:
                {
                    ChangeState(StateType.Move);

                    if (_rb.position.x < target.transform.position.x - _col.size.x)
                    {
                        direction = DIRECTION_RIGHT;
                    }
                    else if (_rb.position.x > target.transform.position.x + _col.size.x)
                    {
                        direction = DIRECTION_LEFT;
                    }
                }
                break;
            case AI.AttackTarget:
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _aiDetectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _aiAttackRange);
    }
}

