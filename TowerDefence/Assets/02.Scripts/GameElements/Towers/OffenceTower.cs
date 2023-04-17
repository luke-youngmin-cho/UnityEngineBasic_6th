using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OffenceTower : Tower, IAttackable
{
    public float damage
    {
        get => damageOrigin;
        private set => damageOrigin = value;
    }

    public float damageModified { get; set; }
    [SerializeField] protected float damageOrigin;
    [SerializeField] protected bool airAttackEnabled;
    [SerializeField] protected Transform rotatePoint;
    protected Collider target;
    public BuffManager<OffenceTower> buffManager { get; private set; }

    protected virtual void Awake()
    {
        buffManager = new BuffManager<OffenceTower>(this);
        damageModified = damageOrigin;
    }

    private void OnDisable()
    {
        buffManager.DeactiveAllBuffs();
    }

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

    float IAttackable.Attack()
    {
        throw new System.NotImplementedException();
    }
}
