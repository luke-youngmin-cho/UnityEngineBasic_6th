using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    public StateIdle(GameObject owner, int id, Func<bool> executionCondition) : base(owner, id, executionCondition)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animator.Play("Idle");
    }
}
