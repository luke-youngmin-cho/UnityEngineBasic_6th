using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] LayerMask _targetMask;


    private void FixedUpdate()
    {
        isDetected =  Physics2D.OverlapBox(transform.position + (Vector3)_offset, _size, 0.0f, _targetMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset, _size);
    }
}
