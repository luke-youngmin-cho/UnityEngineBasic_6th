using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���ư��ٰ� Ʈ���ŵǸ� �ı��Ǵ� �Ѿ�
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
