using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.GameElements.StatSystems;
using System;

namespace RPG.GameElements
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        public Stats stats;

        public event Action<float> onHpChanged;
        public event Action<float> onHpDecreased;
        public event Action<float> onHpIncreased;
        public event Action onHpMax;
        public event Action onHpMin;

        public virtual float hp
        {
            get
            {
                return _hp;
            }
            set
            {
                if (_hp == value)
                    return;

                float _hpOrigin = _hp;
                _hp = value;
                onHpChanged?.Invoke(value);
                if (_hpOrigin < value)
                {
                    onHpIncreased?.Invoke(value - _hpOrigin);
                    if (_hpMax <= value)
                        onHpMax?.Invoke();
                }
                else if (_hpOrigin > value)
                {
                    onHpDecreased?.Invoke(value - _hpOrigin);
                    if (_hpMin >= value)
                        onHpMin?.Invoke();
                }
            }
        }
        protected float _hp;
        public float hpMax => _hpMax;
        protected float _hpMax;
        public float hpMin => _hpMin;
        protected float _hpMin = 0.0f;

        public void Damage(GameObject hitter, float amount)
        {
            hp -= amount;
        }

        public void Heal(GameObject healer, float amount)
        {
            hp += amount;
        }

        protected virtual void Awake()
        {
            hp = hpMax;
        }

        private void FootR() { }
        private void FootL() { }
        protected virtual void StartRightHandCasting() { }
        protected virtual void FinishRightHandCasting() { }

        protected virtual void StartLeftHandCasting() { }
        protected virtual void FinishLeftHandCasting() { }
    }
}
