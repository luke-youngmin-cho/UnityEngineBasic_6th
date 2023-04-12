using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamageable
{
    float hpMin { get; }
    float hpMax { get; }
    float hp { get; }
    event Action<float> onHpChanged;
    event Action onHpMin;
    event Action onHpMax;

    void Damage(GameObject subject, float value);
}
