using RPG.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWrapper : MonoBehaviour
{
    public bool isPreviousMachineFinished => previousMachineHash == exitMachineHash ||
                                             previousMachineHash == 0;

    public bool isPreviousStateFinished => previousStateHash == exitStateHash ||
                                           previousStateHash == 0;

    public int currentMachineHash;
    public int previousMachineHash;
    public int exitMachineHash;
    public int currentStateHash;
    public int previousStateHash;
    public int exitStateHash;
    private Animator _animator;

    public bool isInTransition => _animator.IsInTransition(0);

    public float GetNormalizedTime(int layer = 0)
        => _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;

    public bool GetBool(int id) => _animator.GetBool(id);
    public void SetBool(int id, bool value) => _animator.SetBool(id, value);
    public int GetInt(int id) => _animator.GetInteger(id);
    public void SetInt(int id, int value) => _animator.SetInteger(id, value);
    public float GetFloat(int id) => _animator.GetFloat(id);
    public void SetFloat(int id, float value) => _animator.SetFloat(id, value);
    public void SetTrigger(int id) => _animator.SetTrigger(id);


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InitBehaviours();
    }

    private void InitBehaviours()
    {
        AnimatorMachineMonitor[] monitors = _animator.GetBehaviours<AnimatorMachineMonitor>();
        for (int i = 0; i < monitors.Length; i++)
        {
            monitors[i].onStateMachineEnter += (hash) =>
            {
                previousMachineHash = currentMachineHash;
                currentMachineHash = hash;
            };

            monitors[i].onStateMachineExit += (hash) =>
            {
                exitMachineHash = hash;
            };

            monitors[i].onStateEnter += (hash) =>
            {
                previousStateHash = currentStateHash;
                currentStateHash = hash;
            };

            monitors[i].onStateExit += (hash) =>
            {
                exitStateHash = hash;
            };
        }
    }
}
