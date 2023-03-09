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
        Crouch
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
        GroundDetector groundDetector = owner.GetComponent<GroundDetector>();
        Rigidbody2D rigidBody = owner.GetComponent<Rigidbody2D>();

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
                                       },
                                       hasExitTime: false);
        states.Add((int)StateType.Idle, idle);

        StateMove move = new StateMove(owner: owner,
                                       id: (int)StateType.Move,
                                       executionCondition: () => groundDetector.isDetected,
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => movement.isMovable && movement.isInputValid == false,
                                               (int)StateType.Idle
                                           )
                                       },
                                       hasExitTime: false);
        states.Add((int)StateType.Move, move);

        StateJump jump = new StateJump(owner: owner,
                                       id: (int)StateType.Jump,
                                       executionCondition: () => groundDetector.isDetected &&
                                                                 (currentStateID == (int)StateType.Idle ||
                                                                  currentStateID == (int)StateType.Move),
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => groundDetector.isDetected && 
                                                     rigidBody.velocity.y == 0.0f,
                                               (int)StateType.Idle
                                           ),
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => rigidBody.velocity.y < 0.0f,
                                               (int)StateType.Fall
                                           )
                                       },
                                       hasExitTime: false);
        states.Add((int)StateType.Jump, jump);

        StateFall fall = new StateFall(owner: owner,
                                       id: (int)StateType.Fall,
                                       executionCondition: () => groundDetector.isDetected == false,
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => groundDetector.isDetected &&                                               
                                                     rigidBody.velocity.y < -3.0f,
                                               (int)StateType.Land
                                           ),
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => groundDetector.isDetected &&
                                                     rigidBody.velocity.y >= -3.0f,
                                               (int)StateType.Idle
                                           )
                                       },
                                       hasExitTime: false);
        states.Add((int)StateType.Fall, fall);

        StateLand land = new StateLand(owner: owner,
                                       id: (int)StateType.Land,
                                       executionCondition: () => groundDetector.isDetected,
                                       transitions: new List<KeyValuePair<Func<bool>, int>>()
                                       {
                                           new KeyValuePair<Func<bool>, int>
                                           (
                                               () => true,
                                               (int)StateType.Idle
                                           )
                                       },
                                       hasExitTime:  true);
        states.Add((int)StateType.Land, land);

        StateCrouch crouch = new StateCrouch(owner: owner,
                                             id: (int)StateType.Crouch,
                                             executionCondition: () => groundDetector.isDetected &&
                                                                       (currentStateID == (int)StateType.Idle ||
                                                                        currentStateID == (int)StateType.Move),
                                             transitions: new List<KeyValuePair<Func<bool>, int>>()
                                             {
                                                 new KeyValuePair<Func<bool>, int>
                                                 (
                                                     () => true,
                                                     (int)StateType.Idle
                                                 )
                                             },
                                             hasExitTime: false);
        states.Add((int)StateType.Crouch, crouch);


        currentState = idle;
        currentStateID = (int)StateType.Idle;
        currentState.Execute();
    }
}