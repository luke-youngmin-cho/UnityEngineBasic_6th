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
        Destroy(gameObject);
    }
}
