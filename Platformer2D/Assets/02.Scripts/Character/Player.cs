using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;

    public int hp
    {
        get => _hp;
        set
        {
            if (_hp == value)
                return;

            int prev = _hp;
            _hp = value;

            if (value <= hpMin)
                OnHpMin?.Invoke();
            else if (value >= hpMax)
                OnHpMax?.Invoke();
            else if (value < prev)
                OnHpDecreased?.Invoke(value);
            else if (value > prev)
                OnHpIncreased?.Invoke(value);
        }
    }

    public int hpMax => _hpMax;

    public int hpMin => 0;

    public event Action<int> OnHpDecreased;
    public event Action<int> OnHpIncreased;
    public event Action OnHpMin;
    public event Action OnHpMax;

    private int _hp;
    [SerializeField] private int _hpMax = 100;

    public void Damage(GameObject hitter, int damage)
    {
        hp -= damage;
    }

    private void Awake()
    {
        hp = hpMax;
        stateMachine = new StateMachine(gameObject);
        OnHpDecreased += (value) => stateMachine.ChangeState((int)StateMachine.StateType.Hurt);
        OnHpMin += () => stateMachine.ChangeState((int)StateMachine.StateType.Die);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            stateMachine.ChangeState((int)StateMachine.StateType.Jump);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            stateMachine.ChangeState((int)StateMachine.StateType.LadderDown);
            stateMachine.ChangeState((int)StateMachine.StateType.Crouch);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            stateMachine.ChangeState((int)StateMachine.StateType.LadderUp);
            stateMachine.ChangeState((int)StateMachine.StateType.Ledge);
        }

        if (Input.GetKeyDown(KeyCode.X))
            stateMachine.ChangeState((int)StateMachine.StateType.Slide);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState((int)StateMachine.StateType.Dash);

        stateMachine.UpdateState();
    }
}
