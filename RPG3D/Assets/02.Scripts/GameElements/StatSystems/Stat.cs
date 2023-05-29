using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.GameElements.StatSystems
{
    public class Stat
    {
        public int id;

        public int value
        {
            get => _value;
            set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }
        private int _value;
        public event Action<int> onValueChanged;

        public int valueModified
        {
            get => _valueModified;
            set
            {
                _valueModified = value;
                onValueModifiedChanged?.Invoke(value);
            }
        }
        private int _valueModified;
        public event Action<int> onValueModifiedChanged;
        public List<StatModifier> modifiers = new List<StatModifier>();

        public void AddModifier(StatModifier statModifier)
        {
            modifiers.Add(statModifier);
            valueModified = CalcValueModified();
        }
        public void RemoveModifier(StatModifier statModifier)
        {
            modifiers.Remove(statModifier);
            valueModified = CalcValueModified();
        }
        public int CalcValueModified()
        {
            int sumAddFlat = 0;
            double sumAddPercent = 0.0;
            double sumMulPercent = 0.0;

            foreach (var modifier in modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.None:
                        break;
                    case StatModType.AddFlat:
                        {
                            sumAddFlat += modifier.value;
                        }
                        break;
                    case StatModType.AddPercent:
                        {
                            sumAddPercent += (modifier.value / 100.0);
                        }
                        break;
                    case StatModType.MulPercent:
                        {
                            sumMulPercent *= (modifier.value / 100.0f);
                        }
                        break;
                    default:
                        break;
                }
            }

            return (int)((_value + sumAddFlat) + (_value * sumAddFlat) + (_value * sumMulPercent));
        }
    }
}