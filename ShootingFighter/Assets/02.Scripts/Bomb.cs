using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 앞으로 나아가다가 트리거되면 파괴되는 폭탄
/// </summary>
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Vector3 _dir = Vector3.forward;
    private Transform _tr;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private int _damage = 40;
    [SerializeField] private float _range = 4.0f;
    [SerializeField] private ParticleSystem _explosionEffect;


    //===================================================================
    //                          Private Methods
    //===================================================================

    private void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _tr.Translate(_dir * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _enemyMask) > 0)
        {
            ParticleSystem effect = Instantiate(_explosionEffect, _tr.position, Quaternion.identity);
            Destroy(effect.gameObject, _explosionEffect.main.duration + _explosionEffect.main.startLifetime.constantMax);
            foreach (Collider enemy in Physics.OverlapSphere(_tr.position, _range, _enemyMask))
            {
                enemy.GetComponent<Enemy>().hp 
                    -= (int)((1.0f - Vector3.Distance(_tr.position, enemy.transform.position) / _range) * _damage);
            } 
        }
        Destroy(gameObject);
    }
}
