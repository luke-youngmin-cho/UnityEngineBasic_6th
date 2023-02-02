using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private LayerMask _playerMask;
    private Camera _cam;
    private Transform _tr;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _rb.velocity = Quaternion.Euler(Vector3.up * Random.Range(0.0f, 360.0f)) *  Vector3.back * _speed;
    }

    private void FixedUpdate()
    {
        Vector3 viewPortPoint = _cam.WorldToViewportPoint(_tr.position);

        // 맵 왼쪽/ 오른쪽 경계에 부딪힐때 Z축 대칭변환
        if (viewPortPoint.x < 0.0f ||
            viewPortPoint.x > 1.0f)
        {
            _rb.velocity = new Vector3(-_rb.velocity.x, -_rb.velocity.y, _rb.velocity.z);
        }
        // 맵 위쪽/ 아랫쪽 경계에 부딪힐때 X축 대칭변환
        if (viewPortPoint.y < 0.0f ||
            viewPortPoint.y > 1.0f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, -_rb.velocity.y, -_rb.velocity.z);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _playerMask) > 0)
        {
            FighterFire.instance.bombNum++;
            Destroy(gameObject);
        }
    }
}
