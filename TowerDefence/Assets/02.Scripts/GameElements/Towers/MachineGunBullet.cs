using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MachineGunBullet : Projectile
{
    protected override void OnTargetTriggered(Collider target)
    {
        base.OnTargetTriggered(target);
        target.GetComponent<IDamageable>().Damage(owner, damage);
    }
}
