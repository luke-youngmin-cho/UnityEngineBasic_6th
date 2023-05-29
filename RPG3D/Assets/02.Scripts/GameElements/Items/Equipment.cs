using System.Collections.Generic;
using RPG.GameElements.StatSystems;

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
        LeftHand
    }

    public class Equipment : Item
    {
        public BodyPartType bodyPartType;
        public List<StatModifier> statModifiers;

        public virtual void Equip(Character target)
        {
            foreach (var statModifier in statModifiers)
            {
                target.stats[statModifier.statID].AddModifier(statModifier);
            }
        }

        public virtual void Unequip(Character target)
        {
            foreach (var statModifier in statModifiers)
            {
                target.stats[statModifier.statID].RemoveModifier(statModifier);
            }
        }
    }
}
