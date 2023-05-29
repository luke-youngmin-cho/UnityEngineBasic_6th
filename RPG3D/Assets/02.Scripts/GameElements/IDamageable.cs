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

        public void Damage(GameObject hitter, float damage);
    }
}
