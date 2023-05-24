using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolParameterBehaviour : StateMachineBehaviour
{
    [SerializeField] private string _parameterName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetBool(_parameterName, true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool(_parameterName, false);
    }
}
