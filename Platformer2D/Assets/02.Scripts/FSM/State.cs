using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : IState
{
    public int id { get; set; }

    public bool canExecute => _canExecute.Invoke();

    public List<KeyValuePair<Func<bool>, int>> transitions { get; set; }

    private Func<bool> _canExecute;
    protected GameObject owner;
    protected Animator animator;
    protected Movement movement;

    public State(GameObject owner, int id, Func<bool> executionCondition, List<KeyValuePair<Func<bool>, int>> transitions)
    {
        this.owner = owner;
        this.id = id;
        _canExecute = executionCondition;
        this.transitions = transitions;

        animator = owner.GetComponent<Animator>();
        movement = owner.GetComponent<Movement>();
    }


    public virtual void Execute()
    {
    }

    public virtual void Stop()
    {
    }

    /// <summary>
    /// 현재 상태의 로직을 수행하기위한 함수
    /// </summary>
    /// <returns> 전환하려는 다음 상태 ID </returns>
    public virtual int Update()
    {
        int nextID = id;

        foreach (KeyValuePair<Func<bool>, int> transition  in transitions)
        {
            if (transition.Key.Invoke())
            {
                nextID = transition.Value;
                break;
            }
        }

        return nextID;
    }
}