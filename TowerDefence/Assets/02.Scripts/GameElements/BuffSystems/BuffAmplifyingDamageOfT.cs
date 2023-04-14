public class BuffAmplifyingDamage<T> : IBuff<T>
    where T : IAttackable
{
    private float _gain;

    public BuffAmplifyingDamage(float gain)
    {
        _gain = gain;
    }

    public void OnActive(T target)
    {
        target.damageModified *= _gain;
    }

    public void OnDeactive(T target)
    {
        target.damageModified /= _gain;
    }

    public void OnDuration(T target)
    {
    }
}