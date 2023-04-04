using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
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
    public event Action<float> onHpChanged;
    public event Action onHpMin;
    public event Action onHpMax;

    public float speed;
    public float speedOrigin;

    [SerializeField] private IEnumerator<Transform> _path;
    [SerializeField] private Transform _targetPathPoint;
    private float _posTolerance = 0.1f;

    private Rigidbody _rb;
    private Pathfinder _pathfinder;
    [SerializeField] private Pathfinder.Option _pathfindOption;
    public void SetPath(Transform start, Transform end)
    {
        if (_pathfinder.TryGetOptimizedPath(start, end, out _path, _pathfindOption))
        {
            _path.MoveNext();
            _targetPathPoint = _path.Current;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathfinder = GetComponent<Pathfinder>();
        hp = hpMax;
        speed = speedOrigin;
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
            if (_path.MoveNext())
            {
                _targetPathPoint = _path.Current;
            }
            else
            {
                // todo ->
                // 플레이어체력깎기
                // 자기자신 파괴하기
            }
        }
    }
}
