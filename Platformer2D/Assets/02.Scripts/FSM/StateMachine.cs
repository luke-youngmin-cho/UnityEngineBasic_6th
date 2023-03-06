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
        return true;
    }

    public void UpdateState()
    {
        ChangeState(currentState.Update());
    }

    protected void InitStates()
    {
        states = new Dictionary<int, IState>();
        StateIdle idle = new StateIdle(owner,
                                       (int)StateType.Idle,
                                       () => true);
        states.Add((int)StateType.Idle, idle);

        StateMove move = new StateMove(owner,
                                       (int)StateType.Move,
                                       () => true);
        states.Add((int)StateType.Move, move);
    }
}