using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.GameElements.StatSystems
{
    public enum StatModType
    {
        None,
        AddFlat,
        AddPercent,
        MulPercent
    }

    public class StatModifier
    {
        public int statID;
        public StatModType modType;
        public int value;

        public StatModifier(int statID, StatModType modType, int value)
        {
            this.statID = statID;
            this.modType = modType;
            this.value = value;
        }   
    }
}
