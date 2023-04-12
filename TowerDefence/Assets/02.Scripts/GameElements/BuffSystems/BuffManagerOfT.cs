using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuffManager<T>
    where T : MonoBehaviour
{
    private T _target;
    private Dictionary<IBuff<T>, Coroutine> _buffs;

    public BuffManager(T target)
    {
        _target = target;
        _buffs = new Dictionary<IBuff<T>, Coroutine>();
    }

    public void ActiveBuff(IBuff<T> buff, float duration)
    {
        if (_target == null ||
            _target.gameObject.activeSelf == false)
            return;

        _buffs.Add(buff, _target.StartCoroutine(E_ActiveBuff(buff, duration)));
    }

    private IEnumerator E_ActiveBuff(IBuff<T> buff, float duration)
    {
        buff.OnActive(_target);
        float timeMark = Time.time;
        while (Time.time - timeMark < duration)
        {
            buff.OnDuration(_target);
            yield return null;
        }
        buff.OnDeactive(_target);
        _buffs.Remove(buff);
    }

    public void DeactiveAllBuffs()
    {
        foreach (KeyValuePair<IBuff<T>, Coroutine> buffPair in _buffs)
        {
            if (buffPair.Value != null)
                _target.StopCoroutine(buffPair.Value);
        }
        _buffs.Clear();
    }
}