using UnityEngine;

public class BuffSlowingDown<T> : IBuff<T>
    where T : MonoBehaviour, ISpeed
{
    public float gain
    {
        get => _gain;
        set => _gain = value;
    }
    private float _gain;

    public BuffSlowingDown(float gain)
    {
        _gain = gain;
    }

    public void OnActive(T target)
    {
        target.speedModified /= _gain;
    }

    public void OnDeactive(T target)
    {
        target.speedModified *= _gain;
    }

    public void OnDuration(T target)
    {
    }
}