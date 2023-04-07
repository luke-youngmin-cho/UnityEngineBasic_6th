using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected bool isGuided; // 유도기능
    protected float speed;
    protected float damage;
    protected LayerMask targetMask;
    [SerializeField] protected LayerMask touchMask;
    protected Transform target;

    public void SetUp(bool isGuided, float speed, float damage, LayerMask targetMask, Transform target)
    {
        this.isGuided = isGuided;
        this.speed = speed;
        this.damage = damage;
        this.targetMask = targetMask;
        this.target = target;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (isGuided)
            transform.LookAt(target);

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);
    }
}