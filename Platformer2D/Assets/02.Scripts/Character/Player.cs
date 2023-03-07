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
        _stateMachine.UpdateState();
    }
}
