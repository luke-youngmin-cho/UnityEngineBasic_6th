using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private float _explosionRange = 2.0f;
    protected override void OnTargetTriggered(Collider target)
    {
        base.OnTargetTriggered(target);
        //foreach(IDamageable damageable in Physics.OverlapSphere(transform.position, _explosionRange, targetMask)
        //                                  .Select(x => x.GetComponent<IDamageable>()))
        //{
        //    damageable.Damage(owner, damage);
        //}

        Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRange, targetMask);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(owner, damage);
                enemy.buffManager.ActiveBuff(new BuffBurning<Enemy>(owner, 5.0f, 1.0f), 10.0f);
            }
        }
    }

    protected override void OnTouchTriggered(Collider touched)
    {
        base.OnTouchTriggered(touched);

        Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRange, targetMask);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(owner, damage);
                enemy.buffManager.ActiveBuff(new BuffBurning<Enemy>(owner, 5.0f, 1.0f), 10.0f);
            }
        }
    }
}
