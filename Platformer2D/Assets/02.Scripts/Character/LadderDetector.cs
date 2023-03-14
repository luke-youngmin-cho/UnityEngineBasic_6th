using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    public bool isUpDetected => _upLadder != null && doEscapeUp == false && doEscapeDown == false;
    public bool isDownDetected => _downLadder != null && doEscapeUp == false && doEscapeDown == false;
    public bool doEscapeUp;
    public bool doEscapeDown;

    public float upEscapeOffset;
    public float downEscapeOffset;
    public float upClimbOffset;
    public float downClimbOffset;
    [SerializeField] private LayerMask _targetMask;

    private BoxCollider2D _upLadder;
    private BoxCollider2D _downLadder;

    public Vector2 GetClimbUpStartPos()
    {
        if (_upLadder == null)
            return Vector2.zero;

        float startPosY = _upLadder.transform.position.y + _upLadder.offset.y - _upLadder.size.y / 2.0f + upEscapeOffset;
        startPosY = startPosY > transform.position.y ? startPosY : transform.position.y;
        return new Vector2(_upLadder.transform.position.x + _upLadder.offset.x, startPosY);
    }
    public Vector2 GetClimbDownStartPos()
    {
        if (_downLadder == null)
            return Vector2.zero;

        float startPosY = _downLadder.transform.position.y + _downLadder.offset.y + _downLadder.size.y / 2.0f - downEscapeOffset;
        startPosY = startPosY > transform.position.y ? startPosY : transform.position.y;
        return new Vector2(_downLadder.transform.position.x + _downLadder.offset.x, startPosY);
    }

    private void FixedUpdate()
    {
        doEscapeUp = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * upEscapeOffset, 0.01f) == null;
        doEscapeDown = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * downEscapeOffset, 0.01f) == null;
        _upLadder = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * upClimbOffset, 0.01f) as BoxCollider2D;
        _downLadder = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * downClimbOffset, 0.01f) as BoxCollider2D;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * upEscapeOffset + Vector3.left * 0.1f,
                        transform.position + Vector3.up * upEscapeOffset + Vector3.right * 0.1f);
        Gizmos.DrawLine(transform.position + Vector3.up * downEscapeOffset + Vector3.left * 0.1f,
                        transform.position + Vector3.up * downEscapeOffset + Vector3.right * 0.1f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up * upClimbOffset + Vector3.left * 0.1f,
                        transform.position + Vector3.up * upClimbOffset + Vector3.right * 0.1f);
        Gizmos.DrawLine(transform.position + Vector3.up * downClimbOffset + Vector3.left * 0.1f,
                        transform.position + Vector3.up * downClimbOffset + Vector3.right * 0.1f);
    }
}
