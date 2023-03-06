using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State
{
    public StateMove(GameObject owner, int id, Func<bool> executionCondition) : base(owner, id, executionCondition)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animator.Play("Move");
    }
}
