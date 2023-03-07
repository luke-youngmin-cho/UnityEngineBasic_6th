using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Fall,
        Land,
        Attack,
    }

    public int currentStateID;
    public IState currentState;
    public Dictionary<int, IState> states;
    protected GameObject owner;

    public StateMachine(GameObject owner)
    {
        this.owner = owner;
        InitStates();
    }

    /// <summary>
    /// 다른 상태로 전환하는 함수
    /// </summary>
    /// <param name="nextStateID"> 전환하려는 상태 ID </param>
    /// <returns> 전환 여부 </returns>
    public bool ChangeState(int nextStateID)
    {
        if (currentStateID == nextStateID)
            return false;

        if (states[nextStateID].canExecute == false)
            return false;

        currentState.Stop();
        states[nextStateID].Execute();
        currentStateID = nextStateID;
        currentState = states[nextStateID];
        Debug.Log($"State has changed to {(StateType)nextStateID}");
        return true;
    }

    public void UpdateState()
    {
        ChangeState(currentState.Update());
    }

    protected void InitStates()
    {
        Movement movement = owner.GetComponent<Movement>();

        states = new Dictionary<int, IState>();
        StateIdle idle = new StateIdle(owner: owner,
                                       id: (int)StateType.Idle,
                                       executionCondition: () => true,
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => movement.isMovable && movement.isInputValid,
                                               (int)StateType.Move
                                           )
                                       });
        states.Add((int)StateType.Idle, idle);

        StateMove move = new StateMove(owner: owner,
                                       id: (int)StateType.Move,
                                       executionCondition: () => true,
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => movement.isMovable && movement.isInputValid == false,
                                               (int)StateType.Idle
                                           )
                                       });
        states.Add((int)StateType.Move, move);

        currentState = idle;
        currentStateID = (int)StateType.Idle;
        currentState.Execute();
    }
}