using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target { get; set; }
    [SerializeField] private float _speed = 2.0f;
    private Vector3 _dir = Vector3.forward;
    private Transform _tr;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (target &&
            target.position.z < _tr.position.z - 3.0f)
            _tr.LookAt(target);

        _tr.Translate(_dir * _speed * Time.fixedDeltaTime, Space.Self);
    }
}
