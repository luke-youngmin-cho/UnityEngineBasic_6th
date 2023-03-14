using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            _stateMachine.ChangeState((int)StateMachine.StateType.Jump);

        if (Input.GetKey(KeyCode.DownArrow))
            _stateMachine.ChangeState((int)StateMachine.StateType.Crouch);

        if (Input.GetKey(KeyCode.UpArrow))
            _stateMachine.ChangeState((int)StateMachine.StateType.Ledge);

        if (Input.GetKeyDown(KeyCode.X))
            _stateMachine.ChangeState((int)StateMachine.StateType.Slide);

        _stateMachine.UpdateState();
    }
}
