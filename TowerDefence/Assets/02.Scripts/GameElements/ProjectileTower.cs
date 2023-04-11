using UnityEngine;

public abstract class ProjectileTower : OffenceTower
{
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float reloadTime;
    private float _reloadTimer;

    [Header("Projectile")]
    [SerializeField] private bool _isGuided;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private void Start()
    {
        ObjectPool.instance.Register(new ObjectPool.Element(projectilePrefab.name, projectilePrefab, firePoints.Length));
    }

    private void Update()
    {
        Reload();
    }

    protected void Reload()
    {
        if (_reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                _reloadTimer = reloadTime;
            }
        }
        else
        {
            _reloadTimer -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        GameObject tmp;
        for (int i = 0; i < firePoints.Length; i++)
        {
            tmp = ObjectPool.instance.Take(projectilePrefab.name);
            tmp.GetComponent<Projectile>().SetUp(_isGuided, _speed, _damage, targetMask, target.transform);
            tmp.transform.position = firePoints[i].transform.position;
        }
    }
}