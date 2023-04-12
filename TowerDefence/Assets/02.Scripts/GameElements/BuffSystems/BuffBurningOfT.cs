using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuffBurning<T> : IBuff<T>
    where T : IDamageable
{
    private float _damage;
    private float _period;
    private float _timeMark;
    private GameObject _subject;

    public BuffBurning(GameObject subject, float damage, float period)
    {
        _subject = subject;
        _damage = damage;
        _period = period;
    }


    public void OnActive(T target)
    {
        _timeMark = Time.time;
    }

    public void OnDeactive(T target)
    {
    }

    public void OnDuration(T target)
    {
        if (Time.time - _timeMark > _period)
        {
            target.Damage(_subject, _damage);
            Debug.Log($"[BuffBurning] : {target} 이(가) {_subject}에 의해 불타오르고 있습니다. 현재 체력 : {target.hp}");
            _timeMark = Time.time;
        }
    }
}
