using UnityEngine;

public class BuffAmplifyingDamage<T> : IBuff<T>
    where T : MonoBehaviour, IAttackable
{
    private float _gain;
    private ParticleSystem _effect;

    public BuffAmplifyingDamage(float gain)
    {
        _gain = gain;
    }

    public void OnActive(T target)
    {
        target.damageModified *= _gain;
        _effect = GameObject.Instantiate(Resources.Load<ParticleSystem>("DamageAmplifiedEffect"),
                                         target.transform.position,
                                         Quaternion.identity);
    }

    public void OnDeactive(T target)
    {
        target.damageModified /= _gain;
        GameObject.Destroy(_effect.gameObject);
    }

    public void OnDuration(T target)
    {
    }
}