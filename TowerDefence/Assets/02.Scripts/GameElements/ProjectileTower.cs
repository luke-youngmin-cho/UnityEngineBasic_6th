using UnityEngine;

public abstract class ProjectileTower : OffenceTower
{
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameObject projectilePrefab;

    private void Start()
    {
        ObjectPool.instance.Register(new ObjectPool.Element(projectilePrefab.name, projectilePrefab, firePoints.Length));
    }

    protected override void Attack()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            ObjectPool.instance.Take(projectilePrefab.name).transform.position = firePoints[i].transform.position;
        }
    }
}