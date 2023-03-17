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
    [SerializeField] private int _damage = 20;
    private int _hp;
    [SerializeField] private int _hpMax = 100;
    public bool isInvincible;
    [SerializeField] private float _invincibleDuration = 0.5f;

    [SerializeField] private Vector2 _attackCastCenter;
    [SerializeField] private Vector2 _attackCastSize;
    [SerializeField] private LayerMask _targetMask;
    private Movement _movement;

    public void Damage(GameObject hitter, int damage)
    {
        if (isInvincible)
            return;

        hp -= damage;

        isInvincible = true;
        StartCoroutine(E_ReleaseInvincible());
    }

    IEnumerator E_ReleaseInvincible()
    {
        float timeMark = Time.time;
        while (Time.time - timeMark < _invincibleDuration)
        {
            yield return null;
        }
        isInvincible = false;
    }

    private void Awake()
    {
        hp = hpMax;
        stateMachine = new StateMachine(gameObject);
        OnHpDecreased += (value) => stateMachine.ChangeState((int)StateMachine.StateType.Hurt);
        OnHpMin += () => stateMachine.ChangeState((int)StateMachine.StateType.Die);
        _movement = GetComponent<Movement>();
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

        if (Input.GetKey(KeyCode.A))
            stateMachine.ChangeState((int)StateMachine.StateType.Attack);

        stateMachine.UpdateState();
    }

    private void Hit()
    {
        Collider2D target = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(_attackCastCenter.x * _movement.dir, _attackCastCenter.y), _attackCastSize, 0.0f, _targetMask);
        if (target != null &&
            target.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(gameObject, _damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (stateMachine != null &&
            stateMachine.currentStateID == (int)StateMachine.StateType.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(_attackCastCenter.x * _movement.dir, _attackCastCenter.y, 0.0f), _attackCastSize);
        }
    }
}
