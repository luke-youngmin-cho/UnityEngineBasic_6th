using System.Collections.Generic;
using RPG.GameElements.StatSystems;
using UnityEngine;
using RPG.DataStructures;
using System.Linq;

namespace RPG.GameElements.Items
{
    public enum BodyPartType
    {
        None,
        Head,
        Top,
        Bottom,
        Feet,
        RightHand,
        LeftHand,
        TwoHand
    }

    public class Equipment : Item
    {
        public BodyPartType bodyPartType;
        public List<UKeyValuePair<StatID, int>> statModList;
        public IEnumerable<StatModifier> statModifiers;
        [HideInInspector] public Character owner;
        public virtual void Equip(Character target)
        {
            if (statModifiers == null)
            {
                statModifiers = statModList
                                    .Select(x => new StatModifier(x.key.value, StatModType.AddFlat, x.value));
            }

            foreach (var statModifier in statModifiers)
            {
                target.stats[statModifier.statID].AddModifier(statModifier);
            }
            owner = target;
        }

        public virtual void Unequip(Character target)
        {
            if (owner != target)
                throw new System.Exception($"[Equipment] : Failed to unequip. beacuase {GetType()} is not the owner");

            foreach (var statModifier in statModifiers)
            {
                target.stats[statModifier.statID].RemoveModifier(statModifier);
            }
            owner = null;
        }
    }
}
