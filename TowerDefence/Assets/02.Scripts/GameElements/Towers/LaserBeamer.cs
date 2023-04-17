using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : OffenceTower
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private ParticleSystem _sparkEffect;
    [SerializeField] private Transform _firePoint;

    public int damageStep
    {
        get
        {
            return _damageStep;
        }
        set
        {
            if (_damageStep == value ||
                value > DAMAGE_STEP_MAXIMUM)
                return;

            _damageStep = value;
            _beam.startWidth = 0.03f * (1.0f + value * 0.3f);
            _beam.endWidth = 0.03f * (1.0f + value * 0.3f);
            _sparkEffect.transform.localScale = Vector3.one * (1.0f + value * 0.3f);

            if (value > 0)
            {
                _targetEnemy.buffManager.DeactiveBuff(_buffSlowingDown);
                _buffSlowingDown.gain = _slowGain * (1.0f + value);
                _targetEnemy.buffManager.ActiveBuff(_buffSlowingDown);
            }
        }
    }
    private int _damageStep;
    private const int DAMAGE_STEP_MAXIMUM = 2;
    [SerializeField] private float _damageChargeTime = 0.5f;
    [SerializeField] private float _damageGain = 1.5f;
    [SerializeField] private float _damagePeriod = 0.1f;
    [SerializeField] private float _slowGain = 1.2f;
    private float _damageChargeTimeMark;
    private float _damagePeriodTimeMark;
    public Enemy targetEnemy
    {
        get
        {
            return _targetEnemy;
        }
        set
        {
            if (_targetEnemy == value)
                return;

            if (value == null)
            {
                damageStep = -1;
                _beam.enabled = false;
                _sparkEffect.Stop();
                _targetEnemy.buffManager.DeactiveBuff(_buffSlowingDown);
            }
            else
            {
                damageStep = 0;
                _beam.enabled = true;
                _sparkEffect.Play();

                if (_targetEnemy != null)
                    _targetEnemy.buffManager.DeactiveBuff(_buffSlowingDown);

                _buffSlowingDown.gain = _slowGain * (1.0f + damageStep);
                value.buffManager.ActiveBuff(_buffSlowingDown);
            }
            _targetEnemy = value;
        }
    }
    private Enemy _targetEnemy;
    private BuffSlowingDown<Enemy> _buffSlowingDown;

    override protected void Awake()
    {
        base.Awake();
        _buffSlowingDown = new BuffSlowingDown<Enemy>(_slowGain);
    }

    private void Update()
    {
        Attack();
    }

    protected override void FixedUpdate()
    {
        Collider[] targets = DetectTargets();

        if (targets.Length > 0)
        {
            if (target != targets[0])
            {
                target = targets[0];
                targetEnemy = targets[0].GetComponent<Enemy>();
            }
            rotatePoint.LookAt(target.transform);
        }
        else
        {
            target = null;
            targetEnemy = null;
        }
    }

    protected override void Attack()
    {
        if (_targetEnemy == null)
            return;

        _beam.SetPosition(0, _firePoint.position);
        _beam.SetPosition(1, _targetEnemy.transform.position);

        if (Physics.Raycast(_firePoint.position,
                            (_targetEnemy.transform.position - _firePoint.position).normalized,
                            out RaycastHit hit,
                            Vector3.Distance(_targetEnemy.transform.position, _firePoint.position),
                            targetMask))
        {
            _sparkEffect.transform.position = hit.point;
            _sparkEffect.transform.LookAt(_firePoint);
        }

        if (Time.time - _damageChargeTimeMark > _damageChargeTime)
        {
            damageStep++;
            _damageChargeTimeMark = Time.time;
        }

        if (Time.time - _damagePeriodTimeMark > _damagePeriod)
        {
            _targetEnemy.Damage(gameObject, damageModified * (1.0f + _damageStep * _damageGain));
            _damagePeriodTimeMark = Time.time;
        }
    }
}
