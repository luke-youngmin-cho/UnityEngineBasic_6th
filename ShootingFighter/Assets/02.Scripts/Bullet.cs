using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 앞으로 나아가다가 트리거되면 파괴되는 총알
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    private Vector3 _dir = Vector3.forward;
    private Transform _tr;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private int _damage = 20;

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
            other.GetComponent<Enemy>().hp -= _damage;
        }
        Destroy(gameObject);
    }
}
