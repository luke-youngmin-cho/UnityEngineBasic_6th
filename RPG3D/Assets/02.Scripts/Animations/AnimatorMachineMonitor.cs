using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Animations
{
    public class AnimatorMachineMonitor : StateMachineBehaviour
    {
        public event Action<int> onStateMachineEnter;
        public event Action<int> onStateMachineExit;
        public event Action<int> onStateEnter;
        public event Action<int> onStateExit;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
            onStateMachineEnter?.Invoke(stateMachinePathHash);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineExit(animator, stateMachinePathHash);
            onStateMachineExit?.Invoke(stateMachinePathHash);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            onStateEnter?.Invoke(stateInfo.fullPathHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            onStateExit?.Invoke(stateInfo.fullPathHash);
        }
    }
}