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

            _damageStep = value;
            _beam.startWidth = 0.03f * (1.0f + value * 0.3f);
            _beam.endWidth = 0.03f * (1.0f + value * 0.3f);
            _sparkEffect.transform.localScale = Vector3.one * (1.0f + value * 0.3f);
        }
    }
    private int _damageStep;
    [SerializeField] private float _damageChargeTime = 0.5f;
    [SerializeField] private float _damageGain = 1.5f;
    [SerializeField] private float _damagePeriod = 0.1f;
    [SerializeField] private float _slowGain = 1.2f;
    private float _damageChargeTimeMark;
    public Enemy targetEnemy
    {
        get
        {
            return _targetEnemy;
        }
        set
        {
            if (value == null)
            {
                damageStep = -1;
                _beam.enabled = false;
                _sparkEffect.Stop();
            }
            else
            {
                damageStep = 0;
                _beam.enabled = true;
                _sparkEffect.Play();
            }
            _targetEnemy = value;
        }
    }
    private Enemy _targetEnemy;

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
    }
}
