using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.GameElements
{
    public interface IDamageable
    {
        public float hp { get; }
        public float hpMax { get; }
        public float hpMin { get; }
        public event Action<float> onHpChanged;
        public event Action<float> onHpDecreased;
        public event Action<float> onHpIncreased;
        public event Action onHpMax;
        public event Action onHpMin;

        public void Damage(GameObject hitter, float amount);
        public void Heal(GameObject healer, float amount);
    }
}
