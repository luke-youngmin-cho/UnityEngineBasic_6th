using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hpMin => 0;
    public float hpMax => _hpMax;
    [SerializeField] private float _hpMax;
    public float hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            if (_hp == value)
                return;

            _hp = value;
            onHpChanged?.Invoke(value);

            if (value <= hpMin)
                onHpMin?.Invoke();
            else if (value >= hpMax)
                onHpMax?.Invoke();
        }
    }
    private float _hp;
    private event Action<float> onHpChanged;
    private event Action onHpMin;
    private event Action onHpMax;

    public float speed;
    public float speedOrigin;

    [SerializeField] private List<Transform> _path;
    [SerializeField] private Transform _targetPathPoint;
    private int _currentPathPointIndex;
    private float _posTolerance = 0.03f;

    private Rigidbody _rb;

    public void SetPath(Transform start, Transform end)
    {
        // todo -> find optimized path with pathfinder
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        hp = hpMax;
        speed = speedOrigin;
    }

    private void Start()
    {
        _currentPathPointIndex = 0;
        _targetPathPoint = _path[_currentPathPointIndex + 1];
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = new Vector3(_targetPathPoint.position.x,
                                        _rb.position.y,
                                        _targetPathPoint.position.z);
        Vector3 dir = (targetPos - _rb.position).normalized;

        _rb.rotation = Quaternion.LookRotation(dir);
        _rb.MovePosition(_rb.position + dir * speed * Time.fixedDeltaTime);

        // 타겟포인트도착했는지
        if (Vector3.Distance(_rb.position, targetPos) < _posTolerance)
        {
            if (TryGetNextTargetPoint(out _targetPathPoint))
            {
                _currentPathPointIndex++;
            }
            else
            {
                // todo ->
                // 플레이어체력깎기
                // 자기자신 파괴하기
            }
        }
    }

    private bool TryGetNextTargetPoint(out Transform targetPoint)
    {
        targetPoint = null;

        if (_currentPathPointIndex < _path.Count - 1)
        {
            targetPoint = _path[_currentPathPointIndex + 1];
            return true;
        }

        return false;
    }
}
