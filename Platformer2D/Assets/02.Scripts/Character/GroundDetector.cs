using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected => current;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] LayerMask _targetMask;
    public Collider2D current;
    public Collider2D latest;
    private Collider2D _subject;
    private bool _isSubjectTriggered;

    public bool IsGroundExistBelow()
    {
        if (isDetected == false)
            return false;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _size, 0.0f, Vector2.down, 3.0f, _targetMask);

        if (hits.Length > 1)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != current)
                    return true;
            }
        }
        return false;
    }

    public void IgnoreLatest()
    {
        StartCoroutine(E_IgnoreGround(latest));
    }

    IEnumerator E_IgnoreGround(Collider2D ground)
    {
        Physics2D.IgnoreCollision(_subject, ground, true);
        yield return new WaitUntil(() => _isSubjectTriggered);
        yield return new WaitUntil(() => _isSubjectTriggered == false);
        Physics2D.IgnoreCollision(_subject, ground, false);
    }

    private void Awake()
    {
        _subject = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        current =  Physics2D.OverlapBox(transform.position + (Vector3)_offset, _size, 0.0f, _targetMask);

        if (current != latest &&
            current != null)
        {
            latest = current;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset, _size);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isSubjectTriggered = ((1 << collision.gameObject.layer & _targetMask) > 0) &&
                              (collision == latest);
    }
}
