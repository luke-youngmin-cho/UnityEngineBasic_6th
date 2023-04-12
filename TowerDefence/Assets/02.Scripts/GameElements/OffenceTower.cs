using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OffenceTower : Tower
{
    [SerializeField] protected bool airAttackEnabled;
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

    protected override Collider[] DetectTargets()
    {
        if (airAttackEnabled)
        {
            return Physics.OverlapSphere(transform.position, detectRange, targetMask);
        }
        else
        {
            return Physics.OverlapSphere(transform.position, detectRange, targetMask)
                   .Where(x => x.transform.position.y < transform.position.y)
                   .ToArray();
        }
    }

    protected abstract void Attack();

}
