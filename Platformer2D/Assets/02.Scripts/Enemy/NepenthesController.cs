using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NepenthesController : EnemyController
{
    protected override void Hit()
    {
        base.Hit();
        // todo -> 타겟에게 데미지 주기
        Debug.Log($"{name} 이 때렸다~");
    }
}
