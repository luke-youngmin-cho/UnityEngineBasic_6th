using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected bool isGuided; // 유도기능
    protected float speed;
    protected float damage;
    protected LayerMask targetMask;
    [SerializeField] protected LayerMask touchMask;
    protected Transform target;
    [SerializeField] protected ParticleSystem explosionEffect;

    public void SetUp(bool isGuided, float speed, float damage, LayerMask targetMask, Transform target)
    {
        this.isGuided = isGuided;
        this.speed = speed;
        this.damage = damage;
        this.targetMask = targetMask;
        this.target = target;

        ObjectPool.instance.Register(new ObjectPool.Element(explosionEffect.name, explosionEffect.gameObject, 1));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (target == null ||
            target.gameObject.activeSelf == false)
        {
            ObjectPool.instance.Return(gameObject);
        }

        if (isGuided)
            transform.LookAt(target);

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);
    }

    protected virtual void OnTargetTriggered(Collider target)
    {
        ParticleSystem effect = ObjectPool.instance.Take(explosionEffect.name).GetComponent<ParticleSystem>();
        effect.transform.position = target.ClosestPoint(transform.position);
        effect.transform.rotation = Quaternion.LookRotation(-transform.forward);
        effect.Play();
        ObjectPool.instance.Return(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        ObjectPool.instance.Return(gameObject);
    }

    protected virtual void OnTouchTriggered(Collider touched)
    {
        ParticleSystem effect = ObjectPool.instance.Take(explosionEffect.name).GetComponent<ParticleSystem>();
        effect.transform.position = touched.transform.position;
        effect.Play();
        ObjectPool.instance.Return(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        ObjectPool.instance.Return(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & targetMask) > 0)
        {
            OnTargetTriggered(other);
        }
        else if((1 << other.gameObject.layer & touchMask) > 0)
        {
            OnTouchTriggered(other);
        }
    }
}