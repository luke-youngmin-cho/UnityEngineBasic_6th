using UnityEngine;

public abstract class OffenceTower : Tower
{
    [SerializeField] protected Transform rotatePoint;
    protected Collider target;
    protected virtual void FixedUpdate()
    {
        Collider[] targets = DetectTargets();
        
        if (targets.Length > 0)
        {
            target = targets[0];
            rotatePoint.LookAt(target.transform);
        }
        else
        {
            target = null;
        }
    }

    protected abstract void Attack();

}