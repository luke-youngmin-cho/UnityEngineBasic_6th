using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투기 이동 제어 
/// </summary>
public class FighterMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    private Vector3 _dir;
    private Transform _tr;

    //private float _h;
    //private float _v;


    //===================================================================
    //                          Private Methods
    //===================================================================

    private void Awake()
    {
        //_tr = this.gameObject.GetComponent<Transform>();
        _tr = GetComponent<Transform>();
    }

    private void Update()
    {
        _dir = new Vector3(Input.GetAxisRaw("Horizontal"),
                           0.0f,
                           Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        _tr.Translate(_dir * _speed * Time.fixedDeltaTime);
    }

    //private void Update()
    //{
    //    _h = Input.GetAxisRaw("Horizontal");
    //    _v = Input.GetAxisRaw("Vertical");
    //}
    //
    //private void FixedUpdate()
    //{
    //    transform.Translate(new Vector3(_h, 0.0f, _v).normalized * _speed * Time.fixedDeltaTime);
    //}

}
