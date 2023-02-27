using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private Animator _animator;
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
        InitializeWorkflows();
    }

    private void Update()
    {
        if (_workflows[current].MoveNext() == false)
            _workflows[current].Reset();
    }

    private void InitializeWorkflows()
    {
        _workflows.Add(StateType.Idle, new IdleWorkflow(this));
        _workflows.Add(StateType.Move, new MoveWorkflow(this));

        _conditions.Add(StateType.Idle, () => true);
    }
}

