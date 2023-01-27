using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 말들 따라가게 할 카메라
/// </summary>
public class TargetFollowCamera : MonoBehaviour
{
    /// <summary>
    /// target 이 있을때만 gameobject 활성화
    /// </summary>
    public Transform target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
            gameObject.SetActive(value != null);
        }
    }
    private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _angle;


    //============================================================
    //                      Private Methods
    //============================================================

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(_angle);
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = _target.position + _offset;
    }
}
